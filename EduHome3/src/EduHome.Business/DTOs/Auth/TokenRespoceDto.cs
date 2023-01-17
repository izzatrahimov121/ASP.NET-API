using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.DTOs.Auth;

public class TokenRespoceDto
{
	public string? Token { get; set; }
	public string? Username { get; set; }
	public DateTime Expires { get; set; }
}
