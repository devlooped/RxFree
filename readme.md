![Icon](https://raw.githubusercontent.com/devlooped/RxFree/main/assets/img/icon.png) RxFree
============

Reactive extensions for C# libraries as an ultra-lightweight source-only alternative to the full
[System.Reactive](https://www.nuget.org/packages/System.Reactive) for IObservable&gt;T&lt; producers.

100% dependency-free (source-based) support for library authors exposing IObservable&lt;T&gt; leveraging 
Subject&lt;T&gt;, CompositeDisposable, IObservable&lt;T&gt;.Subscribe extension method overloads,
IObservable&gt;T&lt;.Select/Where/OfType LINQ operators, and others.

[![Version](https://img.shields.io/nuget/vpre/RxFree.svg?color=royalblue)](https://www.nuget.org/packages/RxFree)
[![Downloads](https://img.shields.io/nuget/dt/RxFree.svg?color=darkmagenta)](https://www.nuget.org/packages/RxFree)
[![License](https://img.shields.io/github/license/devlooped/RxFree.svg?color=blue)](https://github.com/devlooped/RxFree/blob/main/license.txt)
[![Build](https://github.com/devlooped/RxFree/workflows/build/badge.svg?branch=main)](https://github.com/devlooped/RxFree/actions)

# Usage

All of the documentation and samples for `Subject<T>` and the provided extension methods 
(i.e. `Subscribe` overloads) that are officially available for `System.Reactive` apply to 
this project as well, since the implementations are heavily based on it (taking them to 
the bare essentials for source-only inclusion, with `Subject<T>` being pretty much exactly 
the same).

# Why

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


# Dogfooding

[![CI Version](https://img.shields.io/endpoint?url=https://shields.kzu.io/vpre/RxFree/main&label=nuget.ci&color=brightgreen)](https://pkg.kzu.io/index.json)
[![Build](https://github.com/devlooped/RxFree/workflows/build/badge.svg?branch=main)](https://github.com/devlooped/RxFree/actions)

We also produce CI packages from branches and pull requests so you can dogfood builds as quickly as they are produced. 

The CI feed is `https://pkg.kzu.io/index.json`. 

The versioning scheme for packages is:

- PR builds: *42.42.42-pr*`[NUMBER]`
- Branch builds: *42.42.42-*`[BRANCH]`.`[COMMITS]`



## Sponsors

[![sponsored](https://raw.githubusercontent.com/devlooped/oss/main/assets/images/sponsors.svg)](https://github.com/sponsors/devlooped) [![clarius](https://raw.githubusercontent.com/clarius/branding/main/logo/byclarius.svg)](https://github.com/clarius)[![clarius](https://raw.githubusercontent.com/clarius/branding/main/logo/logo.svg)](https://github.com/clarius)

*[get mentioned here too](https://github.com/sponsors/devlooped)!*
