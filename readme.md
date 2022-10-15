![Icon](https://raw.githubusercontent.com/devlooped/RxFree/main/assets/img/icon.png) RxFree
============

[![Version](https://img.shields.io/nuget/v/RxFree.svg?color=royalblue)](https://www.nuget.org/packages/RxFree)
[![Downloads](https://img.shields.io/nuget/dt/RxFree.svg?color=darkmagenta)](https://www.nuget.org/packages/RxFree)
[![License](https://img.shields.io/github/license/devlooped/RxFree.svg?color=blue)](https://github.com/devlooped/RxFree/blob/main/license.txt)
[![Build](https://github.com/devlooped/RxFree/workflows/build/badge.svg?branch=main)](https://github.com/devlooped/RxFree/actions)

<!-- #content -->
An ultra-lightweight Rx source-only (C#) nuget to avoid depending on the full 
[System.Reactive](https://www.nuget.org/packages/System.Reactive) for `IObservable<T>` 
producers.

100% dependency-free (source-based) support for library authors exposing IObservable&lt;T&gt; leveraging 
Subject&lt;T&gt;, CompositeDisposable, IObservable&lt;T&gt;.Subscribe extension method overloads,
IObservable&lt;T&gt;.Select/Where/OfType LINQ operators, and others.

# Usage

All of the documentation and samples for `Subject<T>` and the provided extension methods 
(i.e. `Subscribe` overloads) that are officially available for `System.Reactive` apply to 
this project as well, since the implementations are heavily based on it (taking them to 
the bare essentials for source-only inclusion, with `Subject<T>` being pretty much exactly 
the same). 
For example: [Using Subjects](https://docs.microsoft.com/en-us/previous-versions/dotnet/reactive-extensions/hh242970(v=vs.103)).

```csharp
using System;
using System.Reactive.Subjects;

var subject = new Subject<string>();

subject.Subscribe(x => Console.WriteLine($"Got raw value {x}"));

subject.Where(x => int.TryParse(x, out _))
    .Select(x => int.Parse(x))
    .Subscribe(x => Console.WriteLine($"Got number {x} (squared is {x * x})"));

subject.Where(x => bool.TryParse(x, out var value) && value)
    .Subscribe(x => Console.WriteLine($"Got a boolean True"));

while (Console.ReadLine() is var line && !string.IsNullOrEmpty(line))
    subject.OnNext(line);
```

This package is a drop-in replacement for `System.Reactive` if you are only using the 
most common subset of features in it that are also provided in this project.

# Why

For the most part, a producer needs the `Subject<T>` (read more about 
[using subjects](https://docs.microsoft.com/en-us/previous-versions/dotnet/reactive-extensions/hh242970(v=vs.103))) 
and maybe the `ObservableExtensions` that provide `Subscribe` overloads to provide 
lambdas instead of an `IObserver<T>`. Taking the somewhat large and heavy dependency 
on the full [System.Reactive](https://www.nuget.org/packages/System.Reactive) to consume 
just the basics a reusable library needs is overkill in most cases.

In addition to `Subject<T>`, typical activities of a producer are to handle disposables 
and potentially filter/query/convert other observables they consume themselves. 
So the following simple features are provided: 

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

<!-- #content -->

# Dogfooding

[![CI Version](https://img.shields.io/endpoint?url=https://shields.kzu.io/vpre/RxFree/main&label=nuget.ci&color=brightgreen)](https://pkg.kzu.io/index.json)
[![Build](https://github.com/devlooped/RxFree/workflows/build/badge.svg?branch=main)](https://github.com/devlooped/RxFree/actions)

We also produce CI packages from branches and pull requests so you can dogfood builds as quickly as they are produced. 

The CI feed is `https://pkg.kzu.io/index.json`. 

The versioning scheme for packages is:

- PR builds: *42.42.42-pr*`[NUMBER]`
- Branch builds: *42.42.42-*`[BRANCH]`.`[COMMITS]`

<!-- include https://github.com/devlooped/sponsors/raw/main/footer.md -->
# Sponsors 

<!-- sponsors.md -->
[![Clarius Org](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/clarius.png "Clarius Org")](https://github.com/clarius)
[![Christian Findlay](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/MelbourneDeveloper.png "Christian Findlay")](https://github.com/MelbourneDeveloper)
[![C. Augusto Proiete](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/augustoproiete.png "C. Augusto Proiete")](https://github.com/augustoproiete)
[![Kirill Osenkov](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/KirillOsenkov.png "Kirill Osenkov")](https://github.com/KirillOsenkov)
[![MFB Technologies, Inc.](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/MFB-Technologies-Inc.png "MFB Technologies, Inc.")](https://github.com/MFB-Technologies-Inc)
[![SandRock](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/sandrock.png "SandRock")](https://github.com/sandrock)
[![Andy Gocke](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/agocke.png "Andy Gocke")](https://github.com/agocke)
[![Shahzad Huq](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/shahzadhuq.png "Shahzad Huq")](https://github.com/shahzadhuq)


<!-- sponsors.md -->

[![Sponsor this project](https://raw.githubusercontent.com/devlooped/sponsors/main/sponsor.png "Sponsor this project")](https://github.com/sponsors/devlooped)
&nbsp;

[Learn more about GitHub Sponsors](https://github.com/sponsors)

<!-- https://github.com/devlooped/sponsors/raw/main/footer.md -->
