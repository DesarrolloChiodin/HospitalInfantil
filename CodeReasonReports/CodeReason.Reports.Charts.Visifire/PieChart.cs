﻿/************************************************************************
 * Copyright: Hans Wolff
 *
 * License:  This software abides by the LGPL license terms. For further
 *           licensing information please see the top level LICENSE.txt 
 *           file found in the root directory of CodeReason Reports.
 *
 * Author:   Hans Wolff
 *
 ************************************************************************/

namespace CodeReason.Reports.Charts.Visifire
{
    /// <summary>
    /// Represents a pie chart
    /// </summary>
    public class PieChart : ChartBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PieChart()
        {
            _renderAs = global::Visifire.Charts.RenderAs.Pie;
        }
    }
}
