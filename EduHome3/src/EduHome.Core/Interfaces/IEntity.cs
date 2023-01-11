using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.Interfaces;

public interface IEntity
{
	public int Id { get; set; }
	public bool? IsDeleted { get; set; }
}
