using Xunit;

namespace Moq;

public class CalculationStorage
{
    private decimal _value;

    public decimal Value
    {
        get => _value;
        set
        {
            _value = value;
            ValueReset?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool HistoryResetEnabled { get; set; }

    public void ResetHistory()
    {
        // Resetting history
    }

    public event EventHandler ValueReset = (sender, args) => { };
}

public class Calculator
{
    private readonly CalculationStorage _storage;

    public Calculator(CalculationStorage storage)
    {
        _storage = storage;
    }

    public void Reset()
    {
        _storage.Value = default;
    }

    public decimal GetValue()
    {
        return 0;
    }

    private void OnValueReset(object? sender, EventArgs args)
    {
        if (_storage.HistoryResetEnabled)
            _storage.ResetHistory();
    }

    public void Initialize()
    {
        _storage.ValueReset += OnValueReset;
    }

    public void Dispose()
    {
        _storage.ValueReset -= OnValueReset;
    }

    public decimal Add(decimal value) => _storage.Value += value;
}

public class MoqIntro
{
    private readonly Mock<CalculationStorage> _calculatorStorageMock;
    private readonly Calculator _sut;

    public MoqIntro()
    {
        _calculatorStorageMock = new Mock<CalculationStorage>();
        _sut = new Calculator(_calculatorStorageMock.Object);
    }
    #endregion

    #region Overriding property behavior

    #endregion

    #region Verifying  method calss

    #endregion

    #region Properties

    // Set up a property
    [Fact]
    public void Add_WhenCalled_ShouldReturnResult()
    {
        // Arrange 
        var initial = 0;
        var input = 10;
        var expected = 10;
        _calculatorStorageMock.Setup(x => x.Value).Returns(initial);

        // Act
        var actual = _sut.Add(input);

        // Assert
        Assert.Equal(expected, actual);
        _calculatorStorageMock.VerifySet(x => x.Value = expected);
    }

    // Set up getter / setter
    [Fact]
    public void GetValue_WhenCalled_ShouldGetValue()
    {
        // Arrange
        var initial = 123M;
        var expected = initial;
        _calculatorStorageMock.SetupGet(x => x.Value).Returns(initial);
        _calculatorStorageMock.SetupSet(x => x.Value = It.IsAny<decimal>());

        // Act
        var actual = _sut.GetValue();

        // Assert
        Assert.Equal(expected, actual);
        _calculatorStorageMock.VerifyGet(x => x.Value);
        _calculatorStorageMock.VerifySet(x => x.Value = expected);
    }

    // Setup property to track its value
    [Fact]
    public void Add_WhenCalled_ShouldSetValueOnEachCall()
    {
        // Arrange
        var input = 10;
        var expected = 30;
        _calculatorStorageMock.SetupProperty(x => x.Value);

        // Act
        _sut.Add(input);
        _sut.Add(input);
        _sut.Add(input);

        // Assert
        Assert.Equal(expected, _calculatorStorageMock.Object.Value);
    }

    #endregion

    #region Events

    // Setup event add behavior
    [Fact]
    public void Initialize_WhenCalled_ShouldAddEventHandler()
    {
        // Arrange 
        _calculatorStorageMock.SetupAdd(x => x.ValueReset += It.IsAny<EventHandler>());

        // Act 
        _sut.Initialize();

        // Assert
        _calculatorStorageMock.VerifyAdd(x => x.ValueReset += It.IsAny<EventHandler>());
    }

    // Setup event remove behavior
    [Fact]
    public void Dispose_WhenCalled_ShouldRemoveEventHandler()
    {
        // Arrange
        _calculatorStorageMock.SetupRemove(x => x.ValueReset -= It.IsAny<EventHandler>());

        // Act
        _sut.Dispose();

        // Assert
        _calculatorStorageMock.VerifyRemove(x => x.ValueReset -= It.IsAny<EventHandler>());
    }

    // Raising an event
    [Fact]
    public void OnValueReset_WhenValueResetRaisedAndHistoryResetEnabled_ShouldCallHistoryReset()
    {
        // Arrange
        _calculatorStorageMock.SetupProperty(x => x.HistoryResetEnabled, true);

        // Act
        _calculatorStorageMock.Raise(x => x.ValueReset += null, EventArgs.Empty);

        // Assert
        _calculatorStorageMock.Verify(x => x.ResetHistory(), Times.Once);
    }

    // Raising an event automatically
    [Fact]
    public void OnValueReset_WhenValueResetRaisedAndHistoryResetDisabled_ShouldNotCallHistoryReset()
    {
        // Arrange
        _calculatorStorageMock.SetupProperty(x => x.HistoryResetEnabled, false);
        _calculatorStorageMock.SetupSet(x => x.Value = It.IsAny<decimal>()).Raises(x => x.ValueReset += null, EventArgs.Empty);

        // Act
        _calculatorStorageMock.Object.Value = 10;

        // Assert
        _calculatorStorageMock.Verify(x => x.ResetHistory(), Times.Never);
    }

    #endregion

    #region Callbacks

    // Setup callback
    [Fact]
    public void Add_WhenCalled_ShouldCallSetValueOnEachCall()
    {
        // Arrange
        var calls = 0;
        _calculatorStorageMock.SetupSet(x => x.Value = It.IsAny<decimal>()).Callback(() => calls++);

        // Act
        _sut.Add(1);
        _sut.Add(2);
        _sut.Add(3);

        // Assert
        Assert.Equal(3, calls);
    }

    // Setup callback and access invocation arguments
    [Fact]
    public void Add_WhenCalled_ShouldAddValueAndCallCallback()
    {
        // Arrange
        var input = 10;
        var expected = input;
        var actual = 0M;

        _calculatorStorageMock.Setup(x => x.Value).Returns(0);
        _calculatorStorageMock.SetupSet(x => x.Value = It.IsAny<decimal>()).Callback<decimal>(x => actual = x);

        // Act
        _sut.Add(input);

        // Assert
        Assert.Equal(expected, actual);
    }

    // Setup callback before and after the invocation
    [Fact]
    public void Add_WhenCalled_ShouldGetActualValue()
    {
        // Arrange
        var value = 0M;
        var initial = 33M;
        var input = 10M;
        var expected = initial + input;
        var actual = 0M;

        _calculatorStorageMock.SetupProperty(x => x.Value, 0);
        _calculatorStorageMock.SetupGet(x => x.Value).Callback(() => value = initial).Returns(() => value).Callback<decimal>((x) => actual = x);

        // Act
        _sut.Add(input);

        // Assert
        Assert.Equal(expected, actual);
        Assert.Equal(expected, value);
    }

    #endregion

    #region Verification

    // Verify method call
    [Fact]
    public void Reset_WhenCalled_ShouldCallSet()
    {
        // Arrange
        _calculatorStorageMock.SetupSet(x => x.Value = It.IsAny<decimal>());

        // Act
        _sut.Reset();

        // Assert
        _calculatorStorageMock.VerifySet(x => x.Value = It.IsAny<decimal>());
    }

    // Verify method call with custom error message
    [Fact]
    public void Reset_WhenCalled_ShouldCallSetWithCustomErrorMessage()
    {
        // Arrange
        _calculatorStorageMock.SetupSet(x => x.Value = It.IsAny<decimal>());

        // Act
        _sut.Reset();

        // Assert
        _calculatorStorageMock.VerifySet(x => x.Value = It.IsAny<decimal>(), "The value was not set");
    }

    // Verify method call count
    [Fact]
    public void Add_WhenCalled_ShouldSetValueOnlyOnce()
    {
        // Arrange
        _calculatorStorageMock.SetupProperty(x => x.Value);

        // Act
        _sut.Add(10);
        _sut.Add(10);
        _sut.Add(10);
        
        // Assert
        _calculatorStorageMock.Verify(x => x.Value, Times.Exactly(3));
    }

    // Verify no other calls were made
    [Fact]
    public void Add_WhenCalled_ShouldNotCallOtherMethods()
    {
        // Arrange
        _calculatorStorageMock.SetupProperty(x => x.Value);

        // Act
        _sut.Add(10);

        // Assert
        _calculatorStorageMock.Verify(x => x.Value, Times.Once());
        _calculatorStorageMock.VerifyNoOtherCalls();
    }

    // Verify method call with argument exact value
    [Fact]
    public void Add_WhenCalled_ShouldSetExactValue()
    {
        // Arrange
        _calculatorStorageMock.SetupProperty(x => x.Value);

        // Act
        _sut.Add(10);

        // Assert
        _calculatorStorageMock.VerifySet(x => x.Value = 10);
    }

    // Verify method call with argument exact type
    [Fact]
    public void Add_WhenCalled_ShouldSetValue()
    {
        // Arrange
        _calculatorStorageMock.SetupProperty(x => x.Value);

        // Act
        _sut.Add(10);

        // Assert
        _calculatorStorageMock.VerifySet(x => x.Value = It.IsAny<decimal>());
    }
    
    // Verify method call order
    [Fact]
    public void Add_WhenCalled_ShouldCallSetValueThenCallDisplay()
    {
        // Arrange
        var input = 10;
        var sequence = new MockSequence();

        _calculatorStorageMock.InSequence(sequence).SetupSet(x => x.Value = It.IsAny<decimal>());
        _calculatorOutputMock.InSequence(sequence).Setup(x => x.Display(It.IsAny<decimal>()));
        
        // Act
        _sut.Add(input); 
    }

    #endregion
}