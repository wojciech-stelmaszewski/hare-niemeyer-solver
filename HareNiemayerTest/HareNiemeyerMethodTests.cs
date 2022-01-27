using System.Collections.Generic;
using System.Linq;
using Xunit;
using HareNiemeyerMethod;

namespace HareNiemayerTest;

public class HareNiemeyerMethodTests
{
    class Entry
    {
        public string Key { get; set; }
        public decimal Value { get; set; }
    }
    
    [Fact]
    public void ValidateOverflowPrecision2()
    {
        var testData = new List<Entry>
        {
            new Entry { Key = "A", Value = 3},
            new Entry { Key = "B", Value = 27},
            new Entry { Key = "C", Value = 1}
        };

        var hareNiemeyerSolver = new HareNiemeyerSolver(100, 2);
        var result = hareNiemeyerSolver
            .Calculate(testData, datum => datum.Value)
            .ToList();
        
        Assert.Equal(3, result.Count);
        
        Assert.Equal("A", result[0].Item.Key);
        Assert.Equal(3, result[0].Item.Value);
        Assert.Equal(9.68M, result[0].Value);
        
        Assert.Equal("B", result[1].Item.Key);
        Assert.Equal(27, result[1].Item.Value);
        Assert.Equal(87.10M, result[1].Value);
        
        Assert.Equal("C", result[2].Item.Key);
        Assert.Equal(1, result[2].Item.Value);
        Assert.Equal(3.22M, result[2].Value);
        
        Assert.Equal(100, result.Sum(x => x.Value));
    }
    
    [Fact]
    public void ValidateUnderflowPrecision2()
    {
        var testData = new List<Entry>
        {
            new Entry { Key = "A", Value = 8},
            new Entry { Key = "B", Value = 30},
            new Entry { Key = "C", Value = 1}
        };

        var hareNiemeyerSolver = new HareNiemeyerSolver(100, 2);
        var result = hareNiemeyerSolver
            .Calculate(testData, datum => datum.Value)
            .ToList();
        
        Assert.Equal(3, result.Count);
        
        Assert.Equal("A", result[0].Item.Key);
        Assert.Equal(8, result[0].Item.Value);
        Assert.Equal(20.51M, result[0].Value);
        
        Assert.Equal("B", result[1].Item.Key);
        Assert.Equal(30, result[1].Item.Value);
        Assert.Equal(76.92M, result[1].Value);
        
        Assert.Equal("C", result[2].Item.Key);
        Assert.Equal(1, result[2].Item.Value);
        Assert.Equal(2.57M, result[2].Value);
        
        Assert.Equal(100, result.Sum(x => x.Value));
    }
    
    [Fact]
    public void ValidateUnderflowPrecision0()
    {
        var testData = new List<Entry>
        {
            new Entry { Key = "A", Value = 8},
            new Entry { Key = "B", Value = 30},
            new Entry { Key = "C", Value = 1}
        };

        var hareNiemeyerSolver = new HareNiemeyerSolver(100, 0);
        var result = hareNiemeyerSolver
            .Calculate(testData, datum => datum.Value)
            .ToList();
        
        Assert.Equal(3, result.Count);
        
        Assert.Equal("A", result[0].Item.Key);
        Assert.Equal(8, result[0].Item.Value);
        Assert.Equal(20M, result[0].Value);
        
        Assert.Equal("B", result[1].Item.Key);
        Assert.Equal(30, result[1].Item.Value);
        Assert.Equal(77M, result[1].Value);
        
        Assert.Equal("C", result[2].Item.Key);
        Assert.Equal(1, result[2].Item.Value);
        Assert.Equal(3M, result[2].Value);
        
        Assert.Equal(100, result.Sum(x => x.Value));
    }
}