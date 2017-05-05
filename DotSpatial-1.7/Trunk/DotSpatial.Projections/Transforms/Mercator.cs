// ********************************************************************************************************
// Product Name: DotSpatial.Projection
// Description:  The basic module for MapWindow version 6.0
// ********************************************************************************************************
// The contents of this file are subject to the MIT License (MIT)
// you may not use this file except in compliance with the License. You may obtain a copy of the License at
// http://dotspatial.codeplex.com/license
//
// Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF
// ANY KIND, either expressed or implied. See the License for the specific language governing rights and
// limitations under the License.
//
// The original content was ported from the C language from the 4.6 version of Proj4 libraries.
// Frank Warmerdam has released the full content of that version under the MIT license which is
// recognized as being approximately equivalent to public domain.  The original work was done
// mostly by Gerald Evenden.  The latest versions of the C libraries can be obtained here:
// http://trac.osgeo.org/proj/
//
// The Initial Developer of this Original Code is Ted Dunsford. Created 8/13/2009 2:09:02 PM
//
// Contributor(s): (Open source contributors should list themselves and their modifications here).
//        Name         |    Date    |        Comment
// --------------------|------------|------------------------------------------------------------
// Ted Dunsford        |   5/3/2010 |  Updated project to DotSpatial.Projection and license to LGPL
// ********************************************************************************************************

using System;

namespace DotSpatial.Projections.Transforms
{
    /// <summary>
    /// Mercator
    /// </summary>
    public class Mercator : EllipticalTransform
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance of Mercator
        /// </summary>
        public Mercator()
        {
            Proj4Name = "merc";
            Name = "Mercator;Mercator_1SP;Mercator_2SP";
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void EllipticalForward(double[] lp, double[] xy, int startIndex, int numPoints)
        {
            for (int i = startIndex; i < startIndex + numPoints; i++)
            {
                int phi = i * 2 + PHI;
                int lam = i * 2 + LAMBDA;
                int x = i * 2 + X;
                int y = i * 2 + Y;
                if (Math.Abs(Math.Abs(lp[phi]) - HALF_PI) <= EPS10)
                {
                    xy[x] = double.NaN;
                    xy[y] = double.NaN;
                    continue;
                    //throw new ProjectionException(20);
                }
                xy[x] = K0 * lp[lam];
                xy[y] = -K0 * Math.Log(Proj.Tsfn(lp[phi], Math.Sin(lp[phi]), E));
            }
        }

        /// <inheritdoc />
        protected override void SphericalForward(double[] lp, double[] xy, int startIndex, int numPoints)
        {
            for (int i = startIndex; i < startIndex + numPoints; i++)
            {
                int phi = i * 2 + PHI;
                int lam = i * 2 + LAMBDA;
                int x = i * 2 + X;
                int y = i * 2 + Y;
                if (Math.Abs(Math.Abs(lp[phi]) - HALF_PI) <= EPS10)
                {
                    xy[x] = double.NaN;
                    xy[y] = double.NaN;
                    continue;
                    //throw new ProjectionException(20);
                }
                xy[x] = K0 * lp[lam];
                xy[y] = K0 * Math.Log(Math.Tan(FORT_PI + .5 * lp[phi]));
            }
        }

        /// <inheritdoc />
        protected override void EllipticalInverse(double[] xy, double[] lp, int startIndex, int numPoints)
        {
            for (int i = startIndex; i < startIndex + numPoints; i++)
            {
                int phi = i * 2 + PHI;
                int lam = i * 2 + LAMBDA;
                int x = i * 2 + X;
                int y = i * 2 + Y;
                if ((lp[phi] = Proj.Phi2(Math.Exp(-xy[y] / K0), E)) == double.MaxValue)
                {
                    lp[lam] = double.NaN;
                    lp[phi] = double.NaN;
                    continue;
                    //throw new ProjectionException(20);
                }
                lp[lam] = xy[x] / K0;
            }
        }

        /// <inheritdoc />
        protected override void SphericalInverse(double[] xy, double[] lp, int startIndex, int numPoints)
        {
            for (int i = startIndex; i < startIndex + numPoints; i++)
            {
                int phi = i * 2 + PHI;
                int lam = i * 2 + LAMBDA;
                int x = i * 2 + X;
                int y = i * 2 + Y;
                lp[phi] = HALF_PI - 2 * Math.Atan(Math.Exp(-xy[y] / K0));
                lp[lam] = xy[x] / K0;
            }
        }

        /// <summary>
        /// Initializes the transform using the parameters from the specified coordinate system information
        /// </summary>
        /// <param name="projInfo">A ProjectionInfo class contains all the standard and custom parameters needed to initialize this transform</param>
        protected override void OnInit(ProjectionInfo projInfo)
        {
            double phits = 0.0;
            bool isPhits = false;
            if (projInfo.StandardParallel1 != null)
            {
                isPhits = true;
                phits = projInfo.Phi1;
                if (phits >= HALF_PI) throw new ProjectionException(-24);
            }

            if (IsElliptical)
            { /* ellipsoid */
                if (isPhits) K0 = Proj.Msfn(Math.Sin(phits), Math.Cos(phits), Es);
            }
            else
            { /* sphere */
                if (isPhits) K0 = Math.Cos(phits);
            }
        }

        #endregion
    }
}