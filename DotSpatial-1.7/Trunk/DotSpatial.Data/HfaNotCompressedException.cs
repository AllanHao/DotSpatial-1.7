// ********************************************************************************************************
// Product Name: DotSpatial.Data.dll
// Description:  The data access libraries for the DotSpatial project.
// ********************************************************************************************************
// The contents of this file are subject to the MIT License (MIT)
// you may not use this file except in compliance with the License. You may obtain a copy of the License at
// http://dotspatial.codeplex.com/license
//
// Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF
// ANY KIND, either expressed or implied. See the License for the specific language governing rights and
// limitations under the License.
//
// The Original Code is from MapWindow.dll version 6.0
//
// The Initial Developer of this Original Code is Ted Dunsford. Created 2/28/2010 2:59:45 PM
//
// Contributor(s): (Open source contributors should list themselves and their modifications here).
//
// ********************************************************************************************************

using System;

namespace DotSpatial.Data
{
    /// <summary>
    /// HfaNotCompressedException
    /// </summary>
    public class HfaNotCompressedException : ApplicationException
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance of HfaNotCompressedException
        /// </summary>
        public HfaNotCompressedException()
            : base(DataStrings.HfaNotCompressedException)
        {
        }

        #endregion
    }
}