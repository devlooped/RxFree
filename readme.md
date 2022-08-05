![Icon](https://raw.githubusercontent.com/devlooped/RxFree/main/assets/img/icon.png) RxFree
============

An ultra-lightweight Rx source-only (C#) nuget to avoid depending on the full 
[System.Reactive](https://www.nuget.org/packages/System.Reactive) for `IObservable<T>` 
producers.

100% dependency-free (source-based) support for library authors exposing IObservable&lt;T&gt; leveraging 
Subject&lt;T&gt;, CompositeDisposable, IObservable&lt;T&gt;.Subscribe extension method overloads,
IObservable&lt;T&gt;.Select/Where/OfType LINQ operators, and others.

[![Version](https://img.shields.io/nuget/v/RxFree.svg?color=royalblue)](https://www.nuget.org/packages/RxFree)
[![Downloads](https://img.shields.io/nuget/dt/RxFree.svg?color=darkmagenta)](https://www.nuget.org/packages/RxFree)
[![License](https://img.shields.io/github/license/devlooped/RxFree.svg?color=blue)](https://github.com/devlooped/RxFree/blob/main/license.txt)
[![Build](https://github.com/devlooped/RxFree/workflows/build/badge.svg?branch=main)](https://github.com/devlooped/RxFree/actions)

# Usage

All of the documentation and samples for `Subject<T>` and the provided extension methods 
(i.e. `Subscribe` overloads) that are officially available for `System.Reactive` apply to 
this project as well, since the implementations are heavily based on it (taking them to 
the bare essentials for source-only inclusion, with `Subject<T>` being pretty much exactly 
the same). 
For example: [Using Subjects](https://docs.microsoft.com/en-us/previous-versions/dotnet/reactive-extensions/hh242970(v=vs.103)).

```csharp
using System;

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


# Dogfooding

[![CI Version](https://img.shields.io/endpoint?url=https://shields.kzu.io/vpre/RxFree/main&label=nuget.ci&color=brightgreen)](https://pkg.kzu.io/index.json)
[![Build](https://github.com/devlooped/RxFree/workflows/build/badge.svg?branch=main)](https://github.com/devlooped/RxFree/actions)

We also produce CI packages from branches and pull requests so you can dogfood builds as quickly as they are produced. 

The CI feed is `https://pkg.kzu.io/index.json`. 

The versioning scheme for packages is:

- PR builds: *42.42.42-pr*`[NUMBER]`
- Branch builds: *42.42.42-*`[BRANCH]`.`[COMMITS]`


<!-- include docs/footer.md -->
# Sponsors 

<!-- sponsors.md -->
<!-- sponsors -->

<a href='https://github.com/KirillOsenkov'>
  <img src='https://github.com/devlooped/sponsors/raw/main/.github/avatars/KirillOsenkov.svg' alt='Kirill Osenkov' title='Kirill Osenkov'>
</a>
<a href='https://github.com/augustoproiete'>
  <img src='https://github.com/devlooped/sponsors/raw/main/.github/avatars/augustoproiete.svg' alt='C. Augusto Proiete' title='C. Augusto Proiete'>
</a>
<a href='https://github.com/sandrock'>
  <img src='https://github.com/devlooped/sponsors/raw/main/.github/avatars/sandrock.svg' alt='SandRock' title='SandRock'>
</a>
<a href='https://github.com/aws'>
  <img src='https://github.com/devlooped/sponsors/raw/main/.github/avatars/aws.svg' alt='Amazon Web Services' title='Amazon Web Services'>
</a>
<a href='https://github.com/MelbourneDeveloper'>
  <img src='https://github.com/devlooped/sponsors/raw/main/.github/avatars/MelbourneDeveloper.svg' alt='Christian Findlay' title='Christian Findlay'>
</a>
<a href='https://github.com/clarius'>
  <img src='https://github.com/devlooped/sponsors/raw/main/.github/avatars/clarius.svg' alt='Clarius Org' title='Clarius Org'>
</a>
<a href='https://github.com/MFB-Technologies-Inc'>
  <img src='https://github.com/devlooped/sponsors/raw/main/.github/avatars/MFB-Technologies-Inc.svg' alt='MFB Technologies, Inc.' title='MFB Technologies, Inc.'>
</a>

<!-- sponsors -->

<!-- sponsors.md -->

<br>&nbsp;
<a href="https://github.com/sponsors/devlooped" title="Sponsor this project">
  <img src="https://github.com/devlooped/sponsors/blob/main/sponsor.png" />
</a>
<br>

[Learn more about GitHub Sponsors](https://github.com/sponsors)

<!-- docs/footer.md -->
