using System;
using System.Linq;

namespace BattleField7Namespace.ClassesToBeReplaced.Types
{
    /// <summary>
    /// Represent a 2 dimensional coordinate of non-floating point numbers. It's like a basic type for this application.
    /// </summary>
    [Obsolete]
    public class Coord2D
    {
        /// <summary>
        /// The row
        /// </summary>
        private int row;

        /// <summary>
        /// The column
        /// </summary>
        private int column;

        /// <summary>
        /// Initializes a new instance of the <see cref="Coord2D"/> class.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="col">The col.</param>
        public Coord2D(int row, int col)
        {
            this.Row = row;
            this.Column = col;
        }

        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        /// <value>
        /// The row.
        /// </value>
        /// <exception cref="System.ArgumentOutOfRangeException">Row can not be a negative number</exception>
        public int Row
        {
            get
            {
                return this.row;
            }
            set
            {
                this.row = value;
            }
        }


        /// <summary>
        /// Gets or sets the column.
        /// </summary>
        /// <value>
        /// The column.
        /// </value>
        /// <exception cref="System.ArgumentOutOfRangeException">Column can not be a negative number</exception>
        public int Column
        {
            get
            {
                return this.column;
            }
            set
            {
                this.column = value;
            }
        }
    }
}
