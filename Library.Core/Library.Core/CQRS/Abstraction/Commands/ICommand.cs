﻿namespace Library.Core.CQRS.Abstraction.Commands
{
    public interface ICommand : IBaseCommand
    {
    }

    public interface ICommand<TResponse> : IBaseCommand
    {
    }

    public interface IBaseCommand
    {

    }
}
