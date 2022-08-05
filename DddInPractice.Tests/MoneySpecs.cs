using DddInPractice.Logic;
using FluentAssertions;

namespace DddInPractice.Tests;

public class MoneySpecs
{
    [Fact]
    public void Sum_of_two_moneys_produces_correct_result()
    {
        Money money1 = new Money(1, 2, 3, 4, 5, 6);
        Money money2 = new Money(1, 2, 3, 4, 5, 6);

        Money sum = money1 + money2;

        sum.TenRubCount.Should().Be(2);
        sum.FiftyRubCount.Should().Be(4);
        sum.HundredRubCount.Should().Be(6);
        sum.FiveHundredRubCount.Should().Be(8);
        sum.ThousandRubCount.Should().Be(10);
        sum.FiveThousandRubCount.Should().Be(12);
    }

    [Fact]
    public void Two_money_instances_equal_if_contain_the_same_money_amount()
    {
        Money money1 = new Money(1, 2, 3, 4, 5, 6);
        Money money2 = new Money(1, 2, 3, 4, 5, 6);

        money1.Should().Be(money2);
        money1.GetHashCode().Should().Be(money2.GetHashCode());
    }

    [Fact]
    public void Two_money_instances_do_not_equal_if_contain_different_money_amounts()
    {
        Money tenByTen = new Money(10, 0, 0, 0, 0, 0);
        Money hundred = new Money(0, 0, 1, 0, 0, 0);

        hundred.Should().NotBe(tenByTen);
        hundred.GetHashCode().Should().NotBe(tenByTen.GetHashCode());
    }

    [Theory]
    [InlineData(-1, 0, 0, 0, 0, 0)]
    public void Cannot_create_money_with_negative_value(
        int tenRubCount,
        int fiftyRubCount,
        int hundredRubCount,
        int fiveHundredRubCount,
        int thousandRubCount,
        int fiveThousandRubCount)
    {
        Action action = () => new Money(
            tenRubCount,
            fiftyRubCount,
            hundredRubCount,
            fiveHundredRubCount,
            thousandRubCount,
            fiveThousandRubCount);

        action.Should().Throw<InvalidOperationException>();
    }

    [Theory]
    [InlineData(0, 0, 0, 0, 0, 0, 0)]
    [InlineData(1, 0, 0, 0, 0, 0, 10)]
    [InlineData(1, 1, 0, 0, 0, 0, 60)]
    [InlineData(1, 0, 1, 0, 0, 0, 110)]
    [InlineData(1, 0, 0, 1, 0, 0, 510)]
    [InlineData(1, 0, 0, 0, 1, 0, 1010)]
    [InlineData(1, 0, 0, 0, 0, 1, 5010)]
    [InlineData(1, 1, 1, 1, 1, 1, 6660)]
    public void Amount_is_calculated_correctly(
        int tenRubCount,
        int fiftyRubCount,
        int hundredRubCount,
        int fiveHundredRubCount,
        int thousandRubCount,
        int fiveThousandRubCount,
        int expectedAmount)
    {
        Money money = new Money(
            tenRubCount,
            fiftyRubCount,
            hundredRubCount,
            fiveHundredRubCount,
            thousandRubCount,
            fiveThousandRubCount);

        money.Amount.Should().Be(expectedAmount);
    }

    [Fact]
    public void Substraction_of_two_moneys_produces_correct_result()
    {
        Money money1 = new Money(10, 10, 10, 10, 10, 10);
        Money money2 = new Money(1, 2, 3, 4, 5, 6);

        Money result = money1 - money2;

        result.TenRubCount.Should().Be(9);
        result.FiftyRubCount.Should().Be(8);
        result.HundredRubCount.Should().Be(7);
        result.FiveHundredRubCount.Should().Be(6);
        result.ThousandRubCount.Should().Be(5);
        result.FiveThousandRubCount.Should().Be(4);
    }

    [Fact]
    public void Cannot_substract_more_than_exists()
    {
        Money money1 = new Money(0, 1, 0, 0, 0, 0);
        Money money2 = new Money(1, 0, 0, 0, 0, 0);

        Action action = () => {
            Money money = money1 - money2;
        };

        action.Should().Throw<InvalidOperationException>();
    }
}