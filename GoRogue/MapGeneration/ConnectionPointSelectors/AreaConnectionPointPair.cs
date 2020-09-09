﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using SadRogue.Primitives;

namespace GoRogue.MapGeneration.ConnectionPointSelectors
{
    /// <summary>
    /// A pair of points in different areas that have been selected as connection points by an
    /// <see cref="IConnectionPointSelector"/>.
    /// </summary>
    [DataContract]
    [PublicAPI]
    // Tuples do not resolve names properly; function is provided
    [SuppressMessage("ReSharper", "CA2225")]
    public struct AreaConnectionPointPair : IEquatable<AreaConnectionPointPair>
    {
        /// <summary>
        /// The type of component expected.
        /// </summary>
        [DataMember] public readonly Point Area1Position;

        /// <summary>
        /// The tag expected to be associated with a component of the specified type.
        /// </summary>
        [DataMember] public readonly Point Area2Position;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="area1Position"/>
        /// <param name="area2Position"/>
        public AreaConnectionPointPair(Point area1Position, Point area2Position)
        {
            Area1Position = area1Position;
            Area2Position = area2Position;
        }

        /// <summary>
        /// Returns a string representing the two points.
        /// </summary>
        /// <returns/>
        public override string ToString() => $"{Area1Position} <-> {Area2Position}";

        #region Tuple Compatibility

        /// <summary>
        /// Supports C# Deconstruction syntax.
        /// </summary>
        /// <param name="area1Position"/>
        /// <param name="area2Position"/>
        public void Deconstruct(out Point area1Position, out Point area2Position)
        {
            area1Position = Area1Position;
            area2Position = Area2Position;
        }

        /// <summary>
        /// Implicitly converts an AreaConnectionPointPair to an equivalent tuple.
        /// </summary>
        /// <param name="pair"/>
        /// <returns/>
        public static implicit operator (Point area1Position, Point area2Position)(AreaConnectionPointPair pair)
            => pair.ToTuple();

        /// <summary>
        /// Implicitly converts a tuple to its equivalent AreaConnectionPointPair.
        /// </summary>
        /// <param name="tuple"/>
        /// <returns/>
        public static implicit operator AreaConnectionPointPair((Point area1Position, Point area2Position) tuple)
            => FromTuple(tuple);

        /// <summary>
        /// Converts the pair to an equivalent tuple.
        /// </summary>
        /// <returns/>
        public (Point area1Position, Point area2Position) ToTuple() => (Area1Position, Area2Position);

        /// <summary>
        /// Converts the tuple to an equivalent AreaConnectionPointPair.
        /// </summary>
        /// <param name="tuple"/>
        /// <returns/>
        public static AreaConnectionPointPair FromTuple((Point area1Position, Point area2Position) tuple)
            => new AreaConnectionPointPair(tuple.area1Position, tuple.area2Position);
        #endregion

        #region Equality Comparison

        /// <summary>
        /// True if the given pair contains the same points; false otherwise.
        /// </summary>
        /// <param name="other"/>
        /// <returns/>
        public bool Equals(AreaConnectionPointPair other)
            => Area1Position == other.Area1Position && Area2Position == other.Area2Position;

        /// <summary>
        /// True if the given object is a AreaConnectionPointPair and has the same points; false otherwise.
        /// </summary>
        /// <param name="obj"/>
        /// <returns/>
        public override bool Equals(object? obj) => obj is AreaConnectionPointPair pair && Equals(pair);

        /// <summary>
        /// Returns a hash code based on all of the pair's field's.
        /// </summary>
        /// <returns/>
        public override int GetHashCode() => Area1Position.GetHashCode() ^ Area2Position.GetHashCode();

        /// <summary>
        /// True if the given pairs have the same points; false otherwise.
        /// </summary>
        /// <param name="left"/>
        /// <param name="right"/>
        /// <returns/>
        public static bool operator ==(AreaConnectionPointPair left, AreaConnectionPointPair right)
            => left.Equals(right);

        /// <summary>
        /// True if the given pairs have different points for the first and second point, respectively; false otherwise.
        /// </summary>
        /// <param name="left"/>
        /// <param name="right"/>
        /// <returns/>
        public static bool operator !=(AreaConnectionPointPair left, AreaConnectionPointPair right) => !(left == right);
        #endregion
    }
}
