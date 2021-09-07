enum Face
{
    Ace = 1,
    Two = 2,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King
}


class Card
{
    private readonly Suit suit;
    private readonly Face face;

    public Card(Suit suit, Face face)
    {
        this.suit = suit;
        this.face = face;
    }

    public override string ToString()
        => $"{Enum.GetName(typeof(Face), face)} of {Enum.GetName(typeof(Suit), suit)}";
}

enum Suit
{
    Hearts = 1,
    Spades,
    Diamonds,
    Clubs,
}

List<Card> DealHandFromDeck(Stack<Card> deckOfCards,
    int numOfCardsToDeal)
{
    List<Card> temp = new List<Card>();
    for (int i = 0; i < numOfCardsToDeal; i++)
        if (deckOfCards.Count > 0)
            temp.Add(deckOfCards.Pop());
        else
            break;
    return temp;
}

Stack<Card> Shuffle(Stack<Card> deckOfCards)
{
    var random = new Random();
    return new Stack<Card>(deckOfCards.OrderBy(c => random.Next()));
}

string FormatHand(List<Card> dealtHand)
{
    var stringBuilder = new StringBuilder();
    foreach (var card in dealtHand)
        stringBuilder.AppendFormat("{0}{1}", card, Environment.NewLine);
    return stringBuilder.ToString();
}

Stack<Card> deck = new Stack<Card>();
foreach (Suit suit in Enum.GetValues(typeof(Suit)))
{
    foreach (Face face in Enum.GetValues(typeof(Face)))
    {
        deck.Push(new Card(suit, face));
    }
}

List<Card> hand = DealHandFromDeck(deck, 5);
Console.WriteLine("First Hand");
Console.WriteLine(FormatHand(hand));

List<Card> additional = DealHandFromDeck(deck, 5);
Console.WriteLine("Additional Hand");
Console.WriteLine(FormatHand(additional));

List<Card> negative = DealHandFromDeck(deck, -1);
Console.WriteLine("-1 Card Hand");
Console.WriteLine(FormatHand(negative));

var shuffledDeck = Shuffle(deck);

for (int j = 0; j < 10; j++)
{
    var dealtHand = DealHandFromDeck(shuffledDeck, 5);
    Console.WriteLine($"Hand #{j}");
    foreach (var card in dealtHand)
    {
        Console.WriteLine(card.ToString());
    }
    Console.WriteLine(Environment.NewLine);
}
