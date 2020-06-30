﻿using System;
using System.Collections.Generic;
using GoRogue.DiceNotation;
using GoRogue.MapViews;
using GoRogue.Pathing;
using GoRogue.SenseMapping;
using GoRogue.SpatialMaps;
using GoRogue.UnitTests.Mocks;
using SadRogue.Primitives;
using Xunit;
using Xunit.Abstractions;

//using System.Drawing;

namespace GoRogue.UnitTests
{
    public class ManualTestHelpers
    {
        public ManualTestHelpers(ITestOutputHelper output) => _output = output;

        private const int _width = 50;
        private const int _height = 50;
        private readonly ITestOutputHelper _output;

        [Fact]
        public void ManualPrint2DArray()
        {
            var array = new int[10, 10];
            var i = 0;

            for (var x = 0; x < 10; x++)
                for (var y = 0; y < 10; y++)
                {
                    array[x, y] = i;
                    i++;
                }

            _output.WriteLine(array.ExtendToString());
            _output.WriteLine("\nIn Grid:");
            _output.WriteLine(array.ExtendToStringGrid());
            _output.WriteLine("\nIn Grid with 5 field size:");
            _output.WriteLine(array.ExtendToStringGrid(5));
            _output.WriteLine("\nIn Grid with 5 field size left-align:");
            _output.WriteLine(array.ExtendToStringGrid(-5));
        }

        [Fact]
        public void ManualPrintAdjacencyRule()
        {
            _output.WriteLine(AdjacencyRule.Cardinals.ToString());
            _output.WriteLine(AdjacencyRule.EightWay.ToString());
            _output.WriteLine(AdjacencyRule.Diagonals.ToString());
        }

        [Fact]
        public void ManualPrintDiceExpression()
        {
            var expr = Dice.Parse("1d(1d12+4)+5*3");
            _output.WriteLine(expr.ToString());
        }

        [Fact]
        public void ManualPrintDictionary()
        {
            var myDict = new Dictionary<int, string>();

            myDict[1] = "On";
            myDict[3] = "Three";
            myDict[1] = "One";
            myDict[2] = "Two";

            _output.WriteLine(myDict.ExtendToString());
        }

        [Fact]
        public void ManualPrintDisjointSet()
        {
            var s = new DisjointSet(5);

            s.MakeUnion(1, 3);
            s.MakeUnion(2, 4);

            _output.WriteLine(s.ToString());
        }

        [Fact]
        public void ManualPrintFOV()
        {
            var map = MockFactory.Rectangle(_width, _height);
            var myFov = new FOV(map);
            myFov.Calculate(5, 5, 3);

            _output.WriteLine(myFov.ToString());
            _output.WriteLine("");
            _output.WriteLine(myFov.ToString(3));
        }

        [Fact]
        public void ManualPrintList()
        {
            var myList = new List<int>();

            myList.Add(1);
            myList.Add(1);
            myList.Add(3);
            myList.Add(2);

            _output.WriteLine(myList.ExtendToString());
            _output.WriteLine("\nWith bar separators:");
            _output.WriteLine(myList.ExtendToString(separator: " | "));
        }

        [Fact]
        public void ManualPrintMockMap()
        {
            ISettableMapView<bool> map = (ArrayMap<bool>)MockFactory.Rectangle(_width, _height);
            _output.WriteLine(map.ToString());
        }

        [Fact]
        public void ManualPrintMultiSpatialMap()
        {
            var sm = new MultiSpatialMap<MyIDImpl>();

            sm.Add(new MyIDImpl(1), 1, 2);
            sm.Add(new MyIDImpl(2), 1, 2);

            sm.Add(new MyIDImpl(3), 4, 5);

            _output.WriteLine(sm.ToString());
        }

        [Fact]
        public void ManualPrintPath()
        {
            var map = MockFactory.Rectangle(_width, _height);
            var pather = new AStar(map, Distance.Manhattan);
            var path = pather.ShortestPath(1, 2, 5, 6);

            _output.WriteLine(path?.ToString() ?? throw new Exception("Should be a path."));
        }

        [Fact]
        public void ManualPrintRadius()
        {
            _output.WriteLine(Radius.Circle.ToString());
            _output.WriteLine(Radius.Square.ToString());
            _output.WriteLine(Radius.Diamond.ToString());
        }

        [Fact]
        public void ManualPrintRadiusLocationContext()
        {
            var boundlessAreaProv = new RadiusLocationContext((5, 4), 10);
            var boundedAreaProv = new RadiusLocationContext((5, 4), 10, new Rectangle(0, 0, 10, 10));

            _output.WriteLine(boundlessAreaProv + " is boundless!");
            _output.WriteLine(boundedAreaProv + " is bounded...");
        }

        [Fact]
        public void ManualPrintRectangle()
        {
            var rect = new Rectangle(0, 0, 10, 10);
            _output.WriteLine(rect.ToString());
        }

        [Fact]
        public void ManualPrintSenseMap()
        {
            var map = MockFactory.Rectangle(_width, _height);

            var resMap = new ResMap(map);
            var senseMap = new SenseMap(resMap);

            var source = new SenseSource(SourceType.Shadow, 12, 15, 10, Radius.Circle);
            var source2 = new SenseSource(SourceType.Shadow, 18, 15, 10, Radius.Circle);
            senseMap.AddSenseSource(source);
            senseMap.AddSenseSource(source2);

            senseMap.Calculate();

            _output.WriteLine(senseMap.ToString());
            _output.WriteLine("");
            _output.WriteLine(senseMap.ToString(3));
            _output.WriteLine("");
            _output.WriteLine(source.ToString());
        }

        [Fact]
        public void ManualPrintSet()
        {
            var mySet = new HashSet<int>();

            mySet.Add(1);
            mySet.Add(1);
            mySet.Add(3);
            mySet.Add(2);

            _output.WriteLine(mySet.ExtendToString());
            _output.WriteLine("\nWith bar separators:");
            _output.WriteLine(mySet.ExtendToString(separator: " | "));
        }

        [Fact]
        public void ManualPrintSpatialMap()
        {
            var sm = new SpatialMap<MyIDImpl>();

            sm.Add(new MyIDImpl(1), 1, 2);
            sm.Add(new MyIDImpl(2), 1, 3);
            sm.Add(new MyIDImpl(3), 4, 5);

            _output.WriteLine(sm.ToString());
        }
    }
}
