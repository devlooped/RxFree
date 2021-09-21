# RxFree

An ultra-lightweight Rx source-only nuget to avoid depending on the full 
[System.Reactive](https://www.nuget.org/packages/System.Reactive) for `IObservable<T>` 
producers.

For the most part, a producer needs the `Subject<T>` (read more about 
[using subjects](https://docs.microsoft.com/en-us/previous-versions/dotnet/reactive-extensions/hh242970(v=vs.103))) 
and maybe the `ObservableExtensions` that provide `Subscribe` overloads to provide 
lambdas instead of an `IObserver<T>`. 

In addition, typical activities of a producer are to handle disposables and potentially 
filter/query/convert other producers they consume themselves. So the following simple  
features are provided: 

- `Disposable.Empty` and `Disposable.Create(Action)`
- `CompositeDisposable`: allows disposing subscriptions as a group
 - `Subject<T>`: for producing observable sequences
 - Extension methods for `IObservable<T>`:
   * `Subscribe` overloads receiving delegates for onNext, onError and onCompleted
   * `Select`/`Where`/`OfType` LINQ operators

This is what this project provides at the moment, in source form, in your project, as internal 
classes for your own implementation usage, with no external dependencies. They are not even 
visible in the project since NuGet provides them automatically to the compiler, embedded into 
your own assembly, and which you can fully debug as any other code in your project.

[![Build Status](https://dev.azure.com/kzu/builds/_apis/build/status/RxFree?branchName=master)](https://build.azdo.io/kzu/oss/26)
[![Version](https://img.shields.io/nuget/vpre/RxFree.svg?color=royalblue)](https://www.nuget.org/packages/RxFree)
[![Downloads](https://img.shields.io/nuget/dt/RxFree.svg?color=darkmagenta)](https://www.nuget.org/packages/RxFree)
[![License](https://img.shields.io/github/license/kzu/RxFree.svg?color=darkorange)](LICENSE)
