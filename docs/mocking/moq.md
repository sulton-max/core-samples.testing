# Moq

Moq is a mocking library for .NET. It allows you to mock interfaces and classes. It is very easy to use and has a lot of features. It is also very fast.

## Features of Moq

### Initialization 
* [Creating default mock (Loose mock)]() 
* [Creating mock with specified behavior]()
* [Creating partial mock]()
* [Creating mock instance]()
* [Creating recursive mock]()
* [Creating mock repository]()

***Initialization examples***
```csharp
// Creating default mock (Loose mock)
var mock = new Mock<IXDependency>();

### Methods
* Setup method calls
* Setup method returns valeus
* Setup method to throw exception


***Method setup examples*** 
```csharp
// Setup method but do nothing
mock.Setup(x => x.DoSomething());

// Setup metohd to return a value
mock.Setup(x => x.DoSomething()).Returns(true);
```
<br/>

### Properties
* Setup [property]()
* Setup [property getter / setter and verify]() separately
* Setup [property to track its value]()
* Setup [all properties]() 


***Property setup examples***
```csharp
// Setup property
mock.SetupProperty(x => x.Data);

// Setup property with initial value
mock.SetupProperty(x => x.Data, null);

// Setup property getter / setter and verify separately
mock.SetupGet(x => x.Data).Returns(null);
mock.SetupSet(x => x.Data = null);
mock.SetupSet(x => x.Data = It.IsAny<object>());

mock.VerifyGet(x => x.Data);
mock.VerifySet(x => x.Data = null);

// Setup property to track its value
mock.SetupProperty(x => x.Data);

// Setup all properties
mock.SetupAllProperties();
```
<br/>

### Events
* Event handler [add / verify]() setup
* Event handler [remove / verify]() setup
* Event [manual / automatic]() raise setup


***Event setup examples*** 
```csharp
// Event handler add / verify setup
mock.SetupAdd(x => x.DataChanged += It.IsAny<EventHandler>());
mock.VerifyAdd(x => x.DataChanged += It.IsAny<EventHandler>());

// Event handler remove / verify setup
mock.SetupRemove(x => x.DataChanged -= It.IsAny<EventHandler>());
mock.VerifyRemove(x => x.DataChanged -= It.IsAny<EventHandler>());

// Event manual / automatic raise setup
mock.Raise(x => x.DataChanged += null, EventArgs.Empty);
mock.Setup(x => x.DoSomething()).Raises(x => x.DataChanged += null, EventArgs.Empty);
```
<br/>


### Callbacks
* Setup [callback]()
* Callbacks [with arguments]()
* Callback [before / after the invocation]()


***Callback setup examples*** 
```csharp
// Setup callback
mock.Setup(x => x.DoSomething()).Callback(() => calls++);

// Callbacks with arguments
mock.Setup(x => x.DoSomething(It.IsAny<string>())).Callback<string>(x => { inputValue = x});

// Callback before / after the invocation
mock.Setup(x => x.GetSomething()).CallBacke(() => value = randomValue).Returns(() => value).Callback<object>((x) => actual = x);
```
<br/>

### Verification
* Verify method call
* Verify method call with custom error message
* Verify method call count
* Verfiy no other calls were made
* Verify method call with argument value / type 

***Verification examples*** 
```csharp
// Verify method call
mock.Verify(x => x.DoSomething());

// Verify method call with custom error message
mock.Verify(x => x.DoSomething(), "Custom error message");

// Verify method call count
mock.Verify(x => x.DoSomething(), Times.Once);

// Verfiy no other calls were made
mock.VerifyNoOtherCalls();

// Verify method call with argument value
mock.Verify(x => x.DoSomething("test"));

// Verify method call with argument type
mock.Verify(x => x.DoSomething(It.IsAny<string>()));
```
<br/>

## Overall components in glance

<br/>
  <p align=center>
    <img src="https://raw.githubusercontent.com/sulton-max/core-samples.testing.fundamentals/main/docs/mocking/assets/images/moq-events.png">
  </p>
<br/>

---

### Moq ### 

Terminlar 

* Mocking - nusxa qilish, taqlid qilish
* Callback - mocking jarayonida mock qilingan metod yoki propertylar chaqirilganda maxsus logikani ishlatish imkonini beradi

Moq bu .NET uchun mocking kutubxonasi. U interface va klasslarni mocking qilish imkonini beradi. Moq foydalanish uchun juda oson va ko'pgina imkoniyatlarga ega. Hamda u ancha tez ishlaydi.

Moq imkoniyatlari

#### Metodlar ####


### Propertylar ###


### 


