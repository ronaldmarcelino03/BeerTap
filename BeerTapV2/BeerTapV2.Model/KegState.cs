using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerTapV2.Model
{
    public enum KegState
	{
		None,
		New,
        GoingDown,
        AlmostEmpty,
        Empty
    }
}
