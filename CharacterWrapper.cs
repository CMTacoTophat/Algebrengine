using Godot;
using System;
using System.Collections.Generic;
public partial class CharacterWrapper : Control
{
	public enum Char {
		One,
		Two,
		Three,
		Four,
		Five,
		Six,
		Seven,
		Eight,
		Nine,
		Zero,
		X,
		Y,
		Z,
		A,
		B,
		C,
		Plus,
		Minus,
		Multiplication,
		Division,
		Sin,
		Cos,
		Tan,
		Cosecant,
		Secant,
		Cotangent,
		Log,
		Modulo,
		OpenParen,
		CloseParen,
		OpenBrack,
		CloseBrack,
		OpenCurlyBrack,
		CloseCurlyBrack,
		Degree,
		Minute,
		Second,
		Decimal,
		CursorFilled,
		CursorEmpty
	}
	public static Dictionary<string, Tuple<Char, Rect2>> CharacterLookup = new Dictionary<string, Tuple<Char, Rect2>> {
		{"1", new Tuple<Char, Rect2> (Char.One, new Rect2(0, 0, 16, 0))}, //"If either dimension of the region's size is 0, the value from atlas size will be used for that axis instead."
		{"2", new Tuple<Char, Rect2> (Char.Two, new Rect2(16, 0, 16, 0))},
		{"3", new Tuple<Char, Rect2> (Char.Three, new Rect2(32, 0, 16, 0))},
		{"4", new Tuple<Char, Rect2> (Char.Four, new Rect2(48, 0, 16, 0))},
		{"5", new Tuple<Char, Rect2> (Char.Five, new Rect2(64, 0, 16, 0))},
		{"6", new Tuple<Char, Rect2> (Char.Six, new Rect2(80, 0, 16, 0))},
		{"7", new Tuple<Char, Rect2> (Char.Seven, new Rect2(96, 0, 16, 0))},
		{"8", new Tuple<Char, Rect2> (Char.Eight, new Rect2(112, 0, 16, 0))},
		{"9", new Tuple<Char, Rect2> (Char.Nine, new Rect2(128, 0, 16, 0))},
		{"0", new Tuple<Char, Rect2> (Char.Zero, new Rect2(144, 0, 16, 0))},

		{"X", new Tuple<Char, Rect2> (Char.X, new Rect2(160, 0, 16, 0))},
		{"Y", new Tuple<Char, Rect2> (Char.Y, new Rect2(176, 0, 16, 0))},
		{"Z", new Tuple<Char, Rect2> (Char.Z, new Rect2(192, 0, 16, 0))},
		{"A", new Tuple<Char, Rect2> (Char.A, new Rect2(208, 0, 16, 0))},
		{"B", new Tuple<Char, Rect2> (Char.B, new Rect2(224, 0, 16, 0))},
		{"C", new Tuple<Char, Rect2> (Char.C, new Rect2(240, 0, 16, 0))},

		{"PLUS", new Tuple<Char, Rect2> (Char.X, new Rect2(256, 0, 16, 0))},
		{"MINUS", new Tuple<Char, Rect2> (Char.Y, new Rect2(272, 0, 16, 0))},
		{"MULTIPLICATION", new Tuple<Char, Rect2> (Char.Z, new Rect2(288, 0, 16, 0))},
		{"DIVISION", new Tuple<Char, Rect2> (Char.A, new Rect2(304, 0, 16, 0))},

		{"SIN", new Tuple<Char, Rect2> (Char.Sin, new Rect2(320, 0, 32, 0))},
		{"COS", new Tuple<Char, Rect2> (Char.Cos, new Rect2(352, 0, 32, 0))},
		{"TAN", new Tuple<Char, Rect2> (Char.Tan, new Rect2(384, 0, 32, 0))},
		{"CSC", new Tuple<Char, Rect2> (Char.Cosecant, new Rect2(416, 0, 32, 0))},
		{"SEC", new Tuple<Char, Rect2> (Char.Secant, new Rect2(448, 0, 32, 0))},
		{"COT", new Tuple<Char, Rect2> (Char.Cotangent, new Rect2(480, 0, 32, 0))},

		{"LOG", new Tuple<Char, Rect2> (Char.Log, new Rect2(512, 0, 32, 0))},
		{"MODULO", new Tuple<Char, Rect2> (Char.Modulo, new Rect2(544, 0, 32, 0))},

		{"OPENPAREN", new Tuple<Char, Rect2> (Char.OpenParen, new Rect2(576, 0, 16, 0))},
		{"CLOSEPAREN", new Tuple<Char, Rect2> (Char.CloseParen, new Rect2(592, 0, 16, 0))},
		{"OPENBRACK", new Tuple<Char, Rect2> (Char.OpenBrack, new Rect2(608, 0, 16, 0))},
		{"CLOSEBRACK", new Tuple<Char, Rect2> (Char.CloseBrack, new Rect2(624, 0, 16, 0))},
		{"OPENCURLYBRACK", new Tuple<Char, Rect2> (Char.OpenCurlyBrack, new Rect2(640, 0, 16, 0))},
		{"CLOSECURLYBRACK", new Tuple<Char, Rect2> (Char.CloseCurlyBrack, new Rect2(656, 0, 16, 0))},

		{"DEGREE", new Tuple<Char, Rect2> (Char.Degree, new Rect2(672, 0, 16, 0))},
		{"MINUTE", new Tuple<Char, Rect2> (Char.Minute, new Rect2(688, 0, 16, 0))},
		{"SECOND", new Tuple<Char, Rect2> (Char.Second, new Rect2(704, 0, 16, 0))},

		{"CURSORFILLED", new Tuple<Char, Rect2> (Char.CursorFilled, new Rect2(736, 0, 16, 0))},
		{"CURSOREMPTY", new Tuple<Char, Rect2> (Char.CursorEmpty, new Rect2(752, 0, 16, 0))},
	};
	private Texture2D charTexture;
	public Char character;
	public TextureRect drawnChar;
	public int width;
	public CharacterWrapper() {
		//empty so godot can still instantiate (???)
		Setup();
	}
	public CharacterWrapper(string c, Vector2 p, Node can) {
		Setup();
		AtlasTexture a = new AtlasTexture();

		a.Atlas = charTexture;
		Tuple<Char, Rect2> temp;
		if (!CharacterLookup.ContainsKey(c.ToUpper())) {
			temp = CharacterLookup["CURSOREMPTY"];
		} else {
			temp = CharacterLookup[c.ToUpper()];
		}
		a.Region = temp.Item2;
		character = temp.Item1;

		drawnChar = new TextureRect();
		drawnChar.GlobalPosition = p;
		drawnChar.Texture = a;
		drawnChar.Size = temp.Item2.Size;

		can.CallDeferred("add_child", drawnChar);

		width = (int)temp.Item2.Size.X;
	}
	private void Setup() {
		charTexture = (Texture2D)GD.Load("res://CharSheet1.png");
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
