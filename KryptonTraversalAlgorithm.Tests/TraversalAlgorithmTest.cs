using NUnit.Framework;

namespace Tests
{
    public class TraversalAlgorithmTest
    {
        // sorry but i can not do better Mondey 2 AM 
        [SetUp]
        public void TraversalTest()
        {
            var algorithm = new TraversalAlgorithm() ;
            var node = new Node();
            Assert.NotNull(algorithm.Traverse(node));

            Assert.Fail("Write your test");
        }

        [Test]
        public void NextSkillTest()
        {
            ITestAction nextSkill = algorithm.NextSkill(node);
            Assert.NotNull(nextSkill);
           Assert.Fail("Write your test");
        }

        [Test]
        public void MarkAsCompleteTest()
        {
            ITestAction complete = algorithm.MarkAsComplete(node.Id);
            Assert.NotNull(complete);
            Assert.Equals(NodeState.Marked, node.state);
            
            Assert.Fail("Write your test");
        }

        [Test]
        public void IncidenceTest()
        {
            Assert.Fail("Write your test");
        }

        [Test]
        public void StepsTest()
        {

            Assert.IsNull(node.Steps);
            Assert.Fail("Write your test");
        }
    }

}