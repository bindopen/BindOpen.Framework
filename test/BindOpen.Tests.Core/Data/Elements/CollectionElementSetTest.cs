﻿using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Tests.Core.Data.Elements
{
    [TestFixture]
    public class CollectionElementSetTest
    {
        private readonly string _filePath = GlobalVariables.WorkingFolder + "CollectionElementSet.xml";

        private DataElementSet _collectionElementSetA = null;

        private ICollectionElement _collectionElement1 = null;
        private ICollectionElement _collectionElement2 = null;

        [SetUp]
        public void OneTimeSetUp()
        {
            BdoLog log = new BdoLog();

            _collectionElement1 = ElementFactory.CreateCollection(
                "collection1",
                ElementFactory.CreateScalar("key11", "value11"),
                ElementFactory.CreateScalar("key12", "value12"),
                ElementFactory.CreateScalar("key13", "value13")
            );

            _collectionElement2 = ElementFactory.CreateCollection(
                "collection2",
                ElementFactory.CreateScalar("key21", "value21"),
                ElementFactory.CreateScalar("key22", "value22"),
                ElementFactory.CreateScalar("key23", 25),
                ElementFactory.CreateCarrier(
                    "collection2", "tests.core$dbField",
                    ElementFactory.CreateSetFromObject<BdoCarrierConfiguration>(new { path = "file2.txt" }))
            );

            _collectionElementSetA = ElementFactory.CreateSet(_collectionElement1, _collectionElement2);
        }

        [Test]
        public void CreateObjectElementSetTest()
        {
            //Assert.That(
            //    ((string)_collectionElement1.First?["path"] == "file1.txt")
            //    && ((string)_collectionElement2.First?["path"] == "file2.txt")
            //    && ((string)_collectionElement3.First?["path"] == "file3.txt")
            //    && ((string)_collectionElement4.First?["path"] == "file4.txt")
            //    , "Bad collection element creation");

            Assert.That(
                (_collectionElementSetA[1] as CollectionElement)?.GetElementObject<int?>("key23") == 25
                , "Bad collection element set indexation");

            Assert.That(
                _collectionElementSetA.Count == 2, "Bad collection element set creation");
        }

        [Test]
        public void UpdateCheckRepairTest()
        {
            var log = new BdoLog();

            //test update
            //log = _scalarElementSetB.Update(_scalarElementSetA);

            //Assert.That(log.Errors.Count == itemsNumber, "Bad insertion of errors ({0} expected; {1} found)", itemsNumber, log.Errors.Count);
            //Assert.That(log.Exceptions.Count == itemsNumber, "Bad insertion of exceptions ({0} expected; {1} found)", itemsNumber, log.Exceptions.Count);
            //Assert.That(log.Messages.Count == itemsNumber, "Bad insertion of messages ({0} expected; {1} found)", itemsNumber, log.Messages.Count);
            //Assert.That(log.Warnings.Count == itemsNumber, "Bad insertion of warnings ({0} expected; {1} found)", itemsNumber, log.Warnings.Count);
            //Assert.That(log.SubLogs.Count == itemsNumber, "Bad insertion of sub logs ({0} expected; {1} found)", itemsNumber, log.SubLogs.Count);
        }

        [Test]
        public void SaveDataElementSetTest()
        {
            var log = new BdoLog();

            _collectionElementSetA.SaveXml(_filePath, log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Element set saving failed. Result was '" + xml);
        }

        [Test]
        public void LoadDataElementSetTest()
        {
            var log = new BdoLog();

            if (_collectionElementSetA == null || !File.Exists(_filePath))
                SaveDataElementSetTest();

            var elementSet = XmlHelper.Load<DataElementSet>(_filePath, log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Element set loading failed. Result was '" + xml);
        }
    }
}
