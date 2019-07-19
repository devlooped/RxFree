# RxLite

An ultra-lightweight Rx source-only nuget to avoid depending on the full 
[System.Reactive](https://www.nuget.org/packages/System.Reactive) for `IObservable<T>` 
producers.

For the most part, a producer needs the `Subject<T>` (read more about 
[using subjects](https://docs.microsoft.com/en-us/previous-versions/dotnet/reactive-extensions/hh242970(v=vs.103))) 
and maybe the `ObservableExtensions` that provide `Subscribe` overloads to provide 
lambdas instead of an `IObserver<T>`. 

This is what this project provides at the moment, in source form, in your project, as internal 
classes for your own implementation usage, with no external dependencies.

[![Version](https://img.shields.io/nuget/vpre/RxLite.svg)](https://www.nuget.org/packages/RxLite)
[![Downloads](https://img.shields.io/nuget/dt/RxLite.svg)](https://www.nuget.org/packages/RxLite)
[![Build Status](https://dev.azure.com/kzu/builds/_apis/build/status/RxLite?branchName=master)](https://kzu.visualstudio.com/builds/_build/latest?definitionId=20?branchName=master)
[![License](https://img.shields.io/github/license/kzu/RxLite.svg)](https://github.com/kzu/RxLite/blob/master/LICENSE)
