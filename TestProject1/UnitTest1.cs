namespace TestProject1
{
    /*
    [TestMethod]：标记一个方法为测试方法，可以被测试运行器识别和执行23。
    [TestInitialize]：标记一个方法为测试初始化方法，在每个测试方法开始之前执行23。
    [TestCleanup]：标记一个方法为测试清理方法，在每个测试方法结束之后执行23。
    [ExpectedException]：标记一个测试方法期望抛出的异常类型，如果没有抛出或者抛出了不同类型的异常，则该测试失败4。
    [Ignore]：标记一个测试方法或者类为忽略状态，不会被执行4。
    [Timeout]：标记一个测试方法的最大执行时间（毫秒），如果超过则该测试失败4。
    [TestCategory]：标记一个测试方法或者类所属的分类，可以用于筛选和分组4。
     */

    /// <summary>
    /// 单元测试
    /// <seealso cref="https://learn.microsoft.com/zh-cn/dotnet/core/testing/unit-testing-with-mstest"/>
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        ///// <summary>
        ///// 特性的方法中，你可以写一些在该测试项目的最后一个测试类结束后执行的代码，比如恢复系统设置，发送测试报告等。
        ///// </summary>
        //[AssemblyInitialize]
        //public static void AssemblyInit()
        //{

        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[AssemblyCleanup]
        //public static void AssemblyClean()
        //{

        //}

        //[ClassInitialize]
        //public static void ClassInit()
        //{

        //}

        /// <summary>
        /// 特性的方法中，你可以写一些在该测试类的最后一个测试方法结束后执行的代码，比如关闭数据库连接，删除临时文件等。
        /// 一般用ClassCleanUp清理
        /// 必须要是静态的
        /// </summary>
        [ClassCleanup]
        public static void ClassClean()
        {

        }

        [TestInitialize]
        public static void TestInit()
        {

        }

        /// <summary>
        /// 特性的方法中，你可以写一些在每个测试方法结束后执行的代码，比如释放资源，清理数据等。
        /// </summary>
        [TestCleanup]
        public static void TestClean()
        {

        }


        /// <summary>
        /// 测试方法
        /// </summary>
        [TestMethod]
        //[DataRow(true)]
        //[DataRow(false)]
        public void TestMethod1()
        {
            Assert.Fail();
        }
    }
}