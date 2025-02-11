﻿using MediatR;

namespace Application.Features.Reviews.Commands.DeleteReview
{
    public record DeleteReviewCommand(Guid ReviewId) : IRequest<Unit>;
}
