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

// Creating mock with specified behavior
var mock = new Mock<IXDependency>(MockBehavior.Strict);

// Creating partial mock
var mock = new Mock<IXDependency>() { CallBase = true };

// Creating mock instance
var mock = new Mock<IXDependency>();
var value = mock.Object;
var dependencyMock = Mock.Get(value);

// Creating recursive mock
var mock = new Mock<IXDependency>() { DefaultValue = DefaultValue.Mock };
var value = mock.Object.Foo;
var fooMock = Mock.Get(value);

// Creating mock repository
var repository = new MockRepository(MockBehavior.Strict);
var mock = repository.Create<IXDependency>();
var fooMock = repository.Create<IFoo>({MockBehavior.Loose});
```
<br/>

### Methods
* [Setup method calls]()
* [Setup method returns valeus]()
* [Setup method to throw exception]()
* [Setup method with deferred evaluation]()
* [Setup method for sequential calls]()
* [Setup protected method]()

***Method setup examples*** 
```csharp
// Setup method but do nothing
mock.Setup(x => x.DoSomething());

// Setup metohd to return a value
mock.Setup(x => x.DoSomething()).Returns(true);
mock.Setup(x => x.DoSomethingAsync().Result).Returns(true);

// Setup method with deferred evaluation
mock.Setup(x => x.DoSomething()).Returns(() => defferredEvaluationValue);

// Setup method to throw exception
mock.Setup(x => x.DoSomething()).Throws<Exception>();
mock.Setup(x => x.DoSomething()).Throws(exactException);
mock.Setup(x => x.DoSomething()).ThrowsAsync<Exception>();

// Setup method for sequential calls
mock.SetupSequence(x => x.DoSomething()).Pass().Pass().Throws<Exception>();
mock.Setup(x => x.GetSomething()).Returns(true).Returns(false).Throws<Exception>();

// Setup protected method and argument matching
mock.Protected().Setup<bool>("GetSomething").Returns(true);
mock.Protected().Setup<bool>("DoSomething", ItExp.IsAny<object>()).Returns(true);
```
<br/>

### Matching arguments
* [Matching with predicate]()
* [Matching with predicate in certain type]()
* [Matching with any value in certain type]()
* [Matching with not null value in certain type]()
* [Matching with predifined set of values]()
* [Matching with regex / range]()
* [Matching with exclusion of certain values from predefined set of values]()
* [Matching with Custom matcher]()
* [Matching generic type arguments]()

***Matching arguments examples***
```csharp
// Matching with predicate
mock.Setup(x => x.DoSoemthing(It.Is((long y) => y > 200)));

// Matching with predicate in certain type
mock.Setup(x => x.DoSomething(It.Is<long>(y => y > 200)));

// Matching with any value in certain type
mock.Setup(x => x.DoSomething(It.IsAny<long>()));

// Matching with not null value in certain type
mock.Setup(x => x.DoSomething(It.IsNotNull<object>()));

// Matching with predifined set of values
mock.Setup(x => x.DoSomething(It.IsIn<long>(1, 2, 3)));

// Matching with regex / range
mock.Setup(x => x.DoSomething(It.IsRegex("[a-z]+")));
mock.Setup(x => x.DoSomething(It.IsInRange(1, 10, Range.Inclusive)));

// Matching with exclusion of certain values from predefined set of values
mock.Setup(x => x.DoSomething(It.IsNotIn<long>(1, 2, 3)));

// Matching with Custom matcher
mock.Setup(x => x.DoSomething(ItIsLongString()).Returns(true);

public string ItIsLongString()
{
    return Match.Create<string>(x => x.Length > 100);
}

// Matching generic type arguments
mock.Setup(x => x.DoSomething<It.IsAnyType()>()).Returns(true);
mock.Setup(x => x.DoSomething<It.IsSubType()>()).Returns(true);
```
<br/>

### Properties
* [Setup property]()
* [Setup property getter / setter and verify separately]() 
* [Setup property to track its value]()
* [Setup all properties]() 

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
* [Event handler add / verify setup]()
* [Event handler remove / verify setup]()
* [Event manual / automatic raise setup]()

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
* [Setup callback]()
* [Callbacks with arguments]()
* [Callback before / after the invocation]()


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
* [Verify method call]()
* [Verify method call with custom error message]()
* [Verify method call count]()
* [Verfiy no other calls were made]()
* [Verify method call with argument exact value / type]()
* [Verify call order]()

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

// Verify method call order
var sequence = new MockSequence();
mockA.InSequence(sequence).Setup(x => x.DoSomething());
mockB.InSequence(sequence).Setup(x => x.DoSomething());
```
<br/>

### Advanced features
* [Implementing multiple interfaces / in single mock]()

***Advanced features examples***
```csharp
// Implementing multiple interfaces
var mock = new Mock<IXDependency>();
var disposableMock = mock.As<IDisposable>();
disposableMock.Setup(x => x.Dispose());

// Implementing multiple interfaces in single mock
mock.As<IDisposable>().Setup(x => x.Dispose());
```
<br/>

## Overall components in glance

* **Setup**
<br/>
  <p align=center>
    <img src="https://raw.githubusercontent.com/sulton-max/core-samples.testing.fundamentals/main/docs/mocking/assets/images/moq-setup.png" alt="Moq setup components">
  </p>
<br/>

* **Matching**
<br/>
  <p align=center>
    <img src="https://raw.githubusercontent.com/sulton-max/core-samples.testing.fundamentals/main/docs/mocking/assets/images/moq-matching.png" alt="Moq matching components">
  </p>
<br/>

* **Verification**
<br/>
  <p align=center>
    <img src="https://raw.githubusercontent.com/sulton-max/core-samples.testing.fundamentals/main/docs/mocking/assets/images/moq-verification.png" alt="Moq verification components">
  </p>
<br/>

---

# Moq O'zbekcha

Moq bu .NET uchun mocking kutubxonasi. U interfeys va klasslarni mocking qilish imkonini beradi. Moq foydalanish uchun juda oson va ko'pgina imkoniyatlarga ega. Hamda u ancha tez ishlaydi.

## Moq imkoniyatlari

### Mock yaratish
* [Standard mock yaratish (ozod yoki cheklovlarsiz mock)]() 
* [Belgilangan logikali mock yaratish]()
* [Qisman mock yaratish]()
* [Mock obyektini (bitta nusxa) olish]()
* [Rekursiv mock yaratish (obyekt ichidagi obyektlar uchun)]()
* [Mock repository yaratish]()

***Mock yaratish misollari***
```csharp
// Standard mock yaratish (ozod yoki cheklovlarsiz mock)
var mock = new Mock<IXDependency>();

// Belgilangan logikali mock yaratish
var mock = new Mock<IXDependency>(MockBehavior.Strict);

// Qisman mock yaratish
var mock = new Mock<IXDependency>() { CallBase = true };

// Mock obyektini (bitta nusxa) olish
var mock = new Mock<IXDependency>();
var value = mock.Object;
var dependencyMock = Mock.Get(value);

// Rekursiv mock yaratish (obyekt ichidagi obyektlar uchun)
var mock = new Mock<IXDependency>() { DefaultValue = DefaultValue.Mock };
var value = mock.Object.Foo;
var fooMock = Mock.Get(value);

// Mock repository yaratish
var repository = new MockRepository(MockBehavior.Strict);
var mock = repository.Create<IXDependency>();
var fooMock = repository.Create<IFoo>({MockBehavior.Loose});
```
<br/>

### Metodlar
* [Metod chaqiruvini sozlash]()
* [Metod qiymatlarini sozlash]()
* [Method qiymati uchun joyida hisoblanadigan ifodalardan foydalanish]()
* [Metod chaqirvuiga istisno holatni sozlash]()
* [Metodni ketma-ket chaqiruvlar uchun sozlash]()
* [Enkapsulatsiya qilingan methodni sozlash]()

***Metod sozlash misollari*** 
```csharp
// Metod chaqiruvini hech qanday qiymatsiz sozlash
mock.Setup(x => x.DoSomething());

// Metod chaqiruvini qiymat qaytarish uchun sozlash
mock.Setup(x => x.DoSomething()).Returns(true);
mock.Setup(x => x.DoSomethingAsync().Result).Returns(true);

// Method qiymati uchun joyida hisoblanadigan ifodalardan foydalanish
mock.Setup(x => x.DoSomething()).Returns(() => defferredEvaluationValue);

// Metod chaqirvuiga istisno holatni sozlash
mock.Setup(x => x.DoSomething()).Throws<Exception>();
mock.Setup(x => x.DoSomething()).Throws(exactException);
mock.Setup(x => x.DoSomething()).ThrowsAsync<Exception>();

// Metodni ketma-ket chaqiruvlar uchun sozlash
mock.SetupSequence(x => x.DoSomething()).Pass().Pass().Throws<Exception>();
mock.Setup(x => x.GetSomething()).Returns(true).Returns(false).Throws<Exception>();

// Enkapsulatsiya qilingan methodni qiymat qaytarish uchun sozlash
mock.Protected().Setup<bool>("GetSomething").Returns(true);
mock.Protected().Setup<bool>("DoSomething", ItExp.IsAny<object>()).Returns(true);
```
<br/>

### Argumentlar mosligini belgilash (mockdagi metod chaqiruviga mos javob qaytarish uchun)
* [Ifoda bilan belgilash]()
* [Ifoda bilan aniq tip uchun belgilash]()
* [Aniq tipda har qanday qiymat uchun belgilash]()
* [Aniq tipda null bo'lmagan qiymat uchun belgilash]()
* [Ma'lum qiymatlar to'plami bilan belgilash]()
* [Regex yoki qiymat oralig'i bilan belgilash]()
* [Ma'lum qiymatlar to'plamiga kirmasligini belgilash]()
* [Maxsus yaratilgan mutanosiblik belgilovchisidan foydalanish]()
* [Umumiy tip argumenti mutanosibligini belgilash]()

***Argument mosligini belgilash misollari***
```csharp
// Ifoda bilan belgilash
mock.Setup(x => x.DoSoemthing(It.Is((long y) => y > 200)));

// Ifoda bilan aniq tip uchun belgilash
mock.Setup(x => x.DoSomething(It.Is<long>(y => y > 200)));

// Aniq tipda har qanday qiymat uchun belgilash
mock.Setup(x => x.DoSomething(It.IsAny<long>()));

// Aniq tipda null bo'lmagan qiymat uchun belgilash
mock.Setup(x => x.DoSomething(It.IsNotNull<object>()));

// Ma'lum qiymatlar to'plami bilan belgilash
mock.Setup(x => x.DoSomething(It.IsIn<long>(1, 2, 3)));

// Regex yoki qiymat oralig'i bilan belgilash
mock.Setup(x => x.DoSomething(It.IsRegex("[a-z]+")));
mock.Setup(x => x.DoSomething(It.IsInRange(1, 10, Range.Inclusive)));

// Ma'lum qiymatlar to'plamiga kirmasligini belgilash
mock.Setup(x => x.DoSomething(It.IsNotIn<long>(1, 2, 3)));

// Maxsus yaratilgan mosligini belgilovchisidan foydalanish
mock.Setup(x => x.DoSomething(ItIsLongString()).Returns(true);

public string ItIsLongString()
{
    return Match.Create<string>(x => x.Length > 100);
}

// Umumiy tip argumenti mosligini belgilash
mock.Setup(x => x.DoSomething<It.IsAnyType()>()).Returns(true);
mock.Setup(x => x.DoSomething<It.IsSubType()>()).Returns(true);
```
<br/>

### Mock Xususiyatlari (Property)
* [Xususiyatni sozlash]()
* [Xususiyat getter va setter larini sozlash va chaqiruvni tasdiqlash]() 
* [Xusisyat o'z qiymatini kuzatishi uchun sozlash]()
* [Barcha xususiyatlarni sozlash]() 

***Xususiyatni sozlash misollari***
```csharp
// Xususiyatni sozlash
mock.SetupProperty(x => x.Data);

// Xususiyatni bosh qiymat bilan sozlash
mock.SetupProperty(x => x.Data, null);

// Xususiyat getter va setter larini sozlash va chaqiruvni tasdiqlash
mock.SetupGet(x => x.Data).Returns(null);
mock.SetupSet(x => x.Data = null);
mock.SetupSet(x => x.Data = It.IsAny<object>());

mock.VerifyGet(x => x.Data);
mock.VerifySet(x => x.Data = null);

// Xusisyat o'z qiymatini kuzatishi uchun sozlash
mock.SetupProperty(x => x.Data);

// Barcha xususiyatlarni sozlash
mock.SetupAllProperties();
```
<br/>

### Hodisalar (Events)
* [Hodisa boshqaruvchisi (handler) qo'shish / chaqiruvni tasdiqlash (verification)]()
* [Hodisa boshqaruvchisini bo'shatish / chaqiruvni tasdiqlash]()
* [Hodisani shaxsan va avtomatik vujudge keltirish]()

***Hodisani sozlash misollari*** 
```csharp
// Hodisa boshqaruvchisi (handler) qo'shish / chaqiruvni tasdiqlash (verification)
mock.SetupAdd(x => x.DataChanged += It.IsAny<EventHandler>());
mock.VerifyAdd(x => x.DataChanged += It.IsAny<EventHandler>());

// Hodisa boshqaruvchisini bo'shatish / chaqiruvni tasdiqlash
mock.SetupRemove(x => x.DataChanged -= It.IsAny<EventHandler>());
mock.VerifyRemove(x => x.DataChanged -= It.IsAny<EventHandler>());

// Hodisani shaxsan va avtomatik vujudge keltirish
mock.Raise(x => x.DataChanged += null, EventArgs.Empty);
mock.Setup(x => x.DoSomething()).Raises(x => x.DataChanged += null, EventArgs.Empty);
```
<br/>

### Qayta chaqiruvlar (Callbacks)
* [Qayta chaqiruvni sozlash]()
* [Qayta chaqrivuda argumentlardan foydalanish]()
* [Qayta chaqiruvni metod bajarilishidan oldin va keyingi holatlar uchun sozlash]()

***Callback setup examples*** 
```csharp
// Qayta chaqiruvni sozlash
mock.Setup(x => x.DoSomething()).Callback(() => calls++);

// Qayta chaqrivuda argumentlardan foydalanish
mock.Setup(x => x.DoSomething(It.IsAny<string>())).Callback<string>(x => { inputValue = x});

// Qayta chaqiruvni metod bajarilishidan oldin va keyingi holatlar uchun sozlash
mock.Setup(x => x.GetSomething()).CallBacke(() => value = randomValue).Returns(() => value).Callback<object>((x) => actual = x);
```
<br/>


### Propertylar ###


### 


