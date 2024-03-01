using Xunit;

public class Tests{
    [Fact]
    public void Step2(){
        Assert.Equal(3,Server.CalculateMathExpression("1+2"));
        Assert.Equal(6,Server.CalculateMathExpression("8-2"));
        Assert.Equal(13,Server.CalculateMathExpression("8+5"));
        Assert.Equal(0,Server.CalculateMathExpression("100-100"));
        Assert.Equal(234,Server.CalculateMathExpression("231+3"));
        Assert.Equal(-1,Server.CalculateMathExpression("1-2"));
        Assert.Equal(50,Server.CalculateMathExpression("50"));
    }

    [Fact]
    public void Step3(){
        Assert.Equal(57,Server.CalculateMathExpression("12+45"));
        Assert.Equal(158,Server.CalculateMathExpression("123+35"));
        Assert.Equal(240,Server.CalculateMathExpression("256-16"));
    }

    [Fact]
    public void Step4(){
        Assert.Equal(6,Server.CalculateMathExpression("2+2*2"));
        Assert.Equal(18.75,Server.CalculateMathExpression("12+5*7/4-2"));
    }

    [Fact]
    public void Step5(){
        Assert.Equal(15.8,Server.CalculateMathExpression("3.5+12.3"));
        Assert.Equal(4,Server.CalculateMathExpression("0.5*8"));

    }
}