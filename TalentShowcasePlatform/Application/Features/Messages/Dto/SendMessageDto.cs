﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Messages.Dto;

public class SendMessageDto
{
	public Guid ReceiverId { get; set; }
	public string Content { get; set; }
}