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
}