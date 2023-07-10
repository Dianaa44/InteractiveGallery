﻿using FastEndpoints;
using InteractiveGallery.Core.ArtistAggregate;
using InteractiveGallery.SharedKernel.Interfaces;

namespace InteractiveGallery.Web.Endpoints.ContributorEndpoints;
public class Create : Endpoint<CreateContributorRequest, CreateContributorResponse>
{
  private readonly IRepository<Artist> _repository;

  public Create(IRepository<Artist> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Post(CreateContributorRequest.Route);
    AllowAnonymous();
    Options(x => x
      .WithTags("ContributorEndpoints"));
  }
  public override async Task HandleAsync(
    CreateContributorRequest request,
    CancellationToken cancellationToken)
  {
    if (request.Name == null)
    {
      ThrowError("Name is required");
    }

    var newContributor = new Artist(1,request.Name);
    var createdItem = await _repository.AddAsync(newContributor, cancellationToken);
    var response = new CreateContributorResponse
    (
      id: createdItem.Id,
      name: createdItem.Name
    );

    await SendAsync(response, cancellation: cancellationToken);
  }
}
