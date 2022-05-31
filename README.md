# Unity Programming Patterns

Refrences : 
- https://gameprogrammingpatterns.com/contents.html
- https://github.com/Habrador/Unity-Programming-Patterns
- https://sourcemaking.com/design_patterns/creational_patterns

## 1. Introduction

### What is good software architecture?

- We need to be able to make changes to the code with minimum effect to the entire code base.

### How can decoupling help?

- if two pieces of code are coupled, it means you can’t understand one without understanding the other. If you de-couple them, you can reason about either side independently. 
- When we need to change something, the less coupling we have, the less that change ripples throughout the rest of code base.

### At What Cost ?

- abstraction, modularity, design patterns, and software architecture. A well-architected program really is a joyful experience to work in, and everyone loves being more productive.
- Good architecture takes real effort and discipline. 
- You have to think about which parts of the program should be decoupled and introduce abstractions at those points. you have to determine where extensibility should be engineered in so future changes are easier to make.
- Whenever you add a layer of abstraction or a place where extensibility is supported, You’re adding code and complexity to your game that takes time to develop, debug, and maintain.
- That effort pays off if you guess right and end up touching that code later. But predicting the future is hard, and when that modularity doesn’t end up being helpful, it quickly becomes actively harmful. After all, it is more code you have to deal with.
- When people get overzealous about this, you get a codebase whose architecture has spiraled out of control. You’ve got interfaces and abstractions everywhere. Plug-in systems, abstract base classes, virtual methods galore, and all sorts of extension points.
- In theory, all of this decoupling means you have less code to understand before you can extend it, but the layers of abstraction themselves end up filling your mental scratch disk.

### Performance and Speed

- There’s another critique of software architecture and abstraction that you hear sometimes, especially in game development: that it hurts your game’s performance. Many patterns that make your code more flexible rely on virtual dispatch, interfaces, pointers, messages, and other mechanisms that all have at least some runtime cost.
- Making your program more flexible so you can prototype faster will have some performance cost. Likewise, optimizing your code will make it less flexible.
- One compromise is to keep the code flexible until the design settles down and then tear out some of the abstraction later to improve your performance.


### The Good in Bad Code

- Writing well-architected code takes careful thought, and that translates to time. Moreso, maintaining a good architecture over the life of a project takes a lot of effort. You have to treat your codebase like a good camper does their campsite: always try to leave it a little better than you found it.
- This is good when you’re going to be living in and working on that code for a long time. 
- Prototyping — slapping together code that’s just barely functional enough to answer a design question — is a perfectly legitimate programming practice.

### Striking a Balance

- We have a few forces in play:

    1. We want nice architecture so the code is easier to understand over the lifetime of the project.
    2. We want fast runtime performance.
    3. We want to get today’s features done quickly.
- These goals are at least partially in opposition. Good architecture improves productivity over the long term, but maintaining it means every change requires a little more effort to keep things clean.
- The implementation that’s quickest to write is rarely the quickest to run. Instead, optimization takes significant engineering time. Once it’s done, it tends to calcify the codebase: highly optimized code is inflexible and very difficult to change.
- if we cram in features as quickly as we can, our codebase will become a mess of hacks, bugs, and inconsistencies that saps our future productivity.


### Summary 

- Abstraction and decoupling make evolving your program faster and easier, but don’t waste time doing them unless you’re confident the code in question needs that flexibility.
- Think about and design for performance throughout your development cycle, but put off the low-level, nitty-gritty optimizations that lock assumptions into your code until as late as possible.

## 2. Singleton

- Singleton pattern does more harm than good.
- Ensure a class has one instance, and provide a global point of access to it

### 2.1 Singleton Pattern 

#### Restricting a class to one instance

- There are times when a class cannot perform correctly if there's more than one instance of it. The common case is when the class interacts with an external system that maintains its own global state.
- Consider a class that wraps an underlying file system API. Because file operations can take a while to complete, our class performs operations asynchronously. This means multiple operations can be running concurrently, so they must be coordinated with each other. If we start one call to create a file and another one to delete that same file, our wrapper needs to be aware of both to make sure they don’t interfere with each other.
- To do this, a call into our wrapper needs to have access to every previous operation. If users could freely create instances of our class, one instance would have no way of knowing about operations that other instances started. Enter the singleton. It provides a way for a class to ensure at compile time that there is only a single instance of the class.

#### Providing a global point of access

- If there's only one instance, then how can other systems access our file system.
- In addition to creating the single instance, it also provides a globally available method to get it.

### 2.2 Why we use it

* This file system class will ensure there's only one instance. Also we can access it from anywhere without passing it around.

    1. doesn't create the instance if no one uses it : save memory
    2. initialized at run time. common alternative to singleton is a static class. But static class get auto initialized so sometimes they will not have access to data loaded at later time. 
    3. it allows subclassing

### 2.3 Why we regret using it
- In short term, singleton pattern is relativeley benign.

#### It's a global variable
- at early stages, pushing the hardware was more important than software engineering principles. So 'C' & assembly coders used globals and statics without any trouble & shipped good games.
- As games got bigger and more complex, architecture and maintainability started to become the bottleneck. We struggled to ship games not because of hardware limitations, but because of productivity limitations.
 
    1. They make it harder to reason about code - when tracking bug global state could be a problem as there references are everywehere. 
    2. They encourage coupling - When you have the global state, you can access it from anyehere which can couple module that are not related to each other.
    3. They aren't concurreny friendly -  when we creat a chunk of memory that every thread can see and poke at, whether or not they know what other threads are doing to it. That path leads to deadlocks, race conditions, and other hell-to-fix thread-synchronization bugs.
- If singleton pattern is discouraged, how can we architect a game without a global state. There are ways to do that and they are not easy.
- Single isntace is useful. also global access is convenient at first. but will create problem at later stage when you want to make a modification.
- Also in games, lazy initialization is not good as initialiation can take time. (allocating memeory, loading resources).it may delay the desired output.
- So because of these reasons, in games we don't rely on lazy initialization. 
- That solves the lazy initialization problem, but at the expense of discarding several singleton features that do make it better than a raw global variable. With a static instance, we can no longer use polymorphism, and the class must be constructible at static initialization time. Nor can we free the memory that the instance is using when not needed.
- Instead of creating a singleton, what we really have here is a simple static class. That isn’t necessarily a bad thing, but if a static class is all you need, why not get rid of the instance() method entirely and use static functions instead? 

### Unity Implementation




### 2.4 Extra : (These I found on google)

#### Should You use singleton in Unity ?

- game development is not typical business app development, singletons provide an excellent facility for manager classes that suit Unity's component based model. You obviously don't want to go overboard with them, but they replace the need for using Find functions, which are slow and best avoided, especially if you plan on accessing the manager a lot.
-It depends on your implementation. For Managers, or things you know for a fact should only be instantiated once, should use a Singleton Paradigm. The debate about Singletons among programmers, is that those that do not know how or when to use them tend to abuse them and break software design structures. Use them, but know how and when to use them. The best way, is to ask yourself, "Should this script have multiple instances, or should there just be one?".
- The answer is "it depends".For simple projects with small teams or individuals singletons are great. They allow you to access specific game manager type scripts without having to worry about passing around references. They can make your code simpler and cleaner. I note that this is for game manager type objects only. Anything that belongs to a specific game entity, such as player health, should not be a singleton. Singletons are reserved for things that are truly global in scope (like a Facebook manager), not just things that only exist once.For large projects with big team singletons are bad. They create all sorts of dependencies (Dependencies are bad). They also create hard to diagnose bugs, because anything can get into your singleton object and play around with it. You are far better off investigating patterns like Service Locator or Dependency Injection.
- for small projects, singletons seem to work well.
- Designing for scalability from the first will save a lot of time later. If you're working on a small game, obviously a singleton works just fine, but on larger programs you don't want to have to go back and rewrite code constantly. You want to plan for flexibility and scalability from the first.
- Problem with the singleton is not the single instance. It's really about the global access. Anyone can access it from anywhere. It's convinient at first but as the code grows it can become a huge problem.
- Singletons glues subsystem together. Like you have a store screen and you have a player inventory. A lot of people might end up moving the data from the gui screen to the player's inventory using a singleton. That's the kind of thing that might make the shop screen impossible to test in isolation.
- When I feel like I absolutely have to use the Singleton pattern, I do what Unity does with the Camera class. Instead of having only one instance, I have a static member called main. You can create as many instances of that class (lets call it Foo), but you can also have a "singleton" version by using main.

You can even have a special editor for your class which has a check box labelled "This is the main instance" and ticking it sets a hidden flag to true and sets it to false for all other scene objects of that type. In the Awake() method, you can have:
if (_mainFlag == true) { main = this; }

Refrences : 

https://forum.unity.com/threads/singletons-is-it-bad-practice.292655/
https://forum.unity.com/threads/to-singleton-or-not-to-singleton.265049/


1) cause code to be tightly coupled
2) can't be sub-classed
3) avoid Single Responsibility Principle
4) hide dependencies
5) difficult to change later, for example starting with singleton for database connection, but then needing multiple causes a lot of gutting of code and logic.

1) that's a problem, but if designed properly, you can avoid tightly coupling code. Because a singleton has an actual instance, you can still treat it as an instance, and pass it around by reference. Unlike static classes.

2) yes it can be sub-classed, that's its benefit over a static class.

3) this argument is based on a singleton having to create an instance of itself... which is sort of a stretch IMO. And also can be mitigated by having a factory. Which leads back to 1, and 2. If the singleton exists only to enforce a single instance, yet you pass by reference, you can then use a factory to create the instance for passing around, and the factory also can be how which sub class is selected.

4) This really is just repeating number 1.

5) very true, remedied partially again by my answer to 1