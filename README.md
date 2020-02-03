# RxFree

An ultra-lightweight Rx source-only nuget to avoid depending on the full 
[System.Reactive](https://www.nuget.org/packages/System.Reactive) for `IObservable<T>` 
producers.

For the most part, a producer needs the `Subject<T>` (read more about 
[using subjects](https://docs.microsoft.com/en-us/previous-versions/dotnet/reactive-extensions/hh242970(v=vs.103))) 
and maybe the `ObservableExtensions` that provide `Subscribe` overloads to provide 
lambdas instead of an `IObserver<T>`. Additionally, the `CompositeDisposable` 
comes in handy to deal with multiple subscriptions that you might want to 
dispose together.

This is what this project provides at the moment, in source form, in your project, as internal 
classes for your own implementation usage, with no external dependencies.

[![Version](https://img.shields.io/nuget/vpre/RxFree.svg)](https://www.nuget.org/packages/RxFree)
[![Downloads](https://img.shields.io/nuget/dt/RxFree.svg)](https://www.nuget.org/packages/RxFree)
[![Build Status](https://dev.azure.com/kzu/builds/_apis/build/status/RxFree?branchName=master)](https://build.azdo.io/kzu/oss/26)
[![License](https://img.shields.io/github/license/kzu/RxFree.svg)](LICENSE)
