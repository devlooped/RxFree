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

In addition to `Subject<T>`, typical activities of a producer invole handling 
disposables and potentially filtering/querying/converting other observables they 
consume themselves in turn. For that purpose, the following simple features are 
included too: 

- `Subject<T>`: for producing observable sequences
- `Disposable.Empty` and `Disposable.Create(Action)`
- `CompositeDisposable`: allows disposing subscriptions as a group
- Extension methods for `IObservable<T>`:
   * `Subscribe` overloads receiving delegates for onNext, onError and onCompleted
   * `Select`/`Where`/`OfType` LINQ operators

This is what this project provides at the moment, in source form, in your project, as internal 
classes for your own implementation usage, with no external dependencies. They are not even 
visible in the project since the package provides them automatically to the compiler, compiled 
into your own assembly, and which you can fully debug as any other code in your project.

All types are included as `partial` classes, so you can easily add your own code to 
them, and make them public if you need to.

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
[![Kirill Osenkov](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/KirillOsenkov.png "Kirill Osenkov")](https://github.com/KirillOsenkov)
[![MFB Technologies, Inc.](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/MFB-Technologies-Inc.png "MFB Technologies, Inc.")](https://github.com/MFB-Technologies-Inc)
[![Stephen Shaw](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/decriptor.png "Stephen Shaw")](https://github.com/decriptor)
[![Torutek](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/torutek-gh.png "Torutek")](https://github.com/torutek-gh)
[![DRIVE.NET, Inc.](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/drivenet.png "DRIVE.NET, Inc.")](https://github.com/drivenet)
[![Daniel Gnägi](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/dgnaegi.png "Daniel Gnägi")](https://github.com/dgnaegi)
[![Ashley Medway](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/AshleyMedway.png "Ashley Medway")](https://github.com/AshleyMedway)
[![Keith Pickford](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/Keflon.png "Keith Pickford")](https://github.com/Keflon)
[![Thomas Bolon](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/tbolon.png "Thomas Bolon")](https://github.com/tbolon)
[![Kori Francis](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/kfrancis.png "Kori Francis")](https://github.com/kfrancis)
[![Toni Wenzel](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/twenzel.png "Toni Wenzel")](https://github.com/twenzel)
[![Giorgi Dalakishvili](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/Giorgi.png "Giorgi Dalakishvili")](https://github.com/Giorgi)
[![Mike James](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/MikeCodesDotNET.png "Mike James")](https://github.com/MikeCodesDotNET)
[![Dan Siegel](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/dansiegel.png "Dan Siegel")](https://github.com/dansiegel)
[![Reuben Swartz](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/rbnswartz.png "Reuben Swartz")](https://github.com/rbnswartz)
[![Jacob Foshee](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/jfoshee.png "Jacob Foshee")](https://github.com/jfoshee)
[![](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/Mrxx99.png "")](https://github.com/Mrxx99)
[![Eric Johnson](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/eajhnsn1.png "Eric Johnson")](https://github.com/eajhnsn1)
[![Norman Mackay](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/mackayn.png "Norman Mackay")](https://github.com/mackayn)
[![Certify The Web](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/certifytheweb.png "Certify The Web")](https://github.com/certifytheweb)
[![Ix Technologies B.V.](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/IxTechnologies.png "Ix Technologies B.V.")](https://github.com/IxTechnologies)
[![David JENNI](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/davidjenni.png "David JENNI")](https://github.com/davidjenni)
[![Jonathan ](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/Jonathan-Hickey.png "Jonathan ")](https://github.com/Jonathan-Hickey)
[![Oleg Kyrylchuk](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/okyrylchuk.png "Oleg Kyrylchuk")](https://github.com/okyrylchuk)
[![Charley Wu](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/akunzai.png "Charley Wu")](https://github.com/akunzai)
[![Jakob Tikjøb Andersen](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/jakobt.png "Jakob Tikjøb Andersen")](https://github.com/jakobt)
[![Seann Alexander](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/seanalexander.png "Seann Alexander")](https://github.com/seanalexander)
[![Tino Hager](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/tinohager.png "Tino Hager")](https://github.com/tinohager)
[![Mark Seemann](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/ploeh.png "Mark Seemann")](https://github.com/ploeh)
[![Angelo Belchior](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/angelobelchior.png "Angelo Belchior")](https://github.com/angelobelchior)
[![Ken Bonny](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/KenBonny.png "Ken Bonny")](https://github.com/KenBonny)
[![Simon Cropp](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/SimonCropp.png "Simon Cropp")](https://github.com/SimonCropp)
[![agileworks-eu](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/agileworks-eu.png "agileworks-eu")](https://github.com/agileworks-eu)
[![](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/sorahex.png "")](https://github.com/sorahex)
[![Zheyu Shen](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/arsdragonfly.png "Zheyu Shen")](https://github.com/arsdragonfly)
[![Vezel](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/vezel-dev.png "Vezel")](https://github.com/vezel-dev)


<!-- sponsors.md -->

[![Sponsor this project](https://raw.githubusercontent.com/devlooped/sponsors/main/sponsor.png "Sponsor this project")](https://github.com/sponsors/devlooped)
&nbsp;

[Learn more about GitHub Sponsors](https://github.com/sponsors)

<!-- https://github.com/devlooped/sponsors/raw/main/footer.md -->
