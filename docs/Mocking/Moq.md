### Moq ###

Moq is a mocking library for .NET. It allows you to mock interfaces and classes. It is very easy to use and has a lot of features. It is also very fast.

Features of Moq

#### Setup method ####

* Setup method calls
```csharp
mock.Setup(x => x.DoSomething());
```
* Setup method returns valeus
```csharp
mock.Setup(x => x.DoSomething()).Returns(true);
```
* Setup method to throw exception




Overriding property behavior
* Overriding indexers
* Overriding events
* 

**Events**
* Event handler [add / verify]() setup
* Event handler [remove / verify]() setup
* Event [manual / automatic]() raise setup

<br/>
  <p align=center>
    <img src="https://raw.githubusercontent.com/sulton-max/core-samples.testing.fundamentals/main/docs/mocking/assets/images/moq-events.png" width="600px">
  </p>
<br/>

[Events in Moq](/assets/images/moq-events.png)

**Callbacks**
* Setup callback
* Callbacks with arguments
* Callback before / after the invocation



---

### Moq ### 

Terminlar 

* Mocking - nusxa qilish, taqlid qilish
* Callback - mocking jarayonida mock qilingan metod yoki propertylar chaqirilganda maxsus logikani ishlatish imkoini beradi

Moq bu .NET uchun mocking kutubxonasi. U interface va klasslarni mocking qilish imkonini beradi. Moq foydalanish uchun juda oson va ko'pgina imkoniyatlarga ega. Hamda u ancha tez ishlaydi.

Moq imkoniyatlari

#### Metodlar ####


### Propertylar ###


### 


