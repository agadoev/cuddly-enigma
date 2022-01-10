using NUnit.Framework;
using Infrastructure.InMemoryDataAcces;
using Infrastructure;
using System;

namespace Tests.IntegrationTests {


    public class DataBaseBuilder {

        public IDbContext ctx;

        public DataBaseBuilder UseInMemoryDbContext() {
            // this.ctx = new InMemoryDbContext();
            return this;
        }
    }

    public class SimpleTest {
        [Test]
        public void Test1() {
            var ctx = new InMemoryDbContext();
        }
    }
}