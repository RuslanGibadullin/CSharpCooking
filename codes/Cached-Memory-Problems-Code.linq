<Query Kind="Program">
  <Namespace>System.Runtime.InteropServices</Namespace>
</Query>

void Main()
{
	const int N = 1_000_000_000;
	{
		Thread[] T = new Thread[2];
		Stopwatch time = new Stopwatch();
		time.Start();
		A o = new A(0, 1);
		T[0] = new Thread(() =>
		{
			for (int i = 0; i < N; i++)
				o.x += i;
		});
		T[1] = new Thread(() =>
		{
			for (int i = 0; i < N; i++)
				o.y += i;
		});
		T[0].Start();
		T[1].Start();
		T[0].Join();
		T[1].Join();
		time.Stop();
		Console.WriteLine($"Time with class A: {time.ElapsedMilliseconds} msec.");
	}
	{
		Thread[] T = new Thread[2];
		Stopwatch time = new Stopwatch();
		time.Start();
		B o = new B(0, 1);
		T[0] = new Thread(() =>
		{
			for (int i = 0; i < N; i++)
				o.x += i;
		});
		T[1] = new Thread(() =>
		{
			for (int i = 0; i < N; i++)
				o.y += i;
		});
		T[0].Start();
		T[1].Start();
		T[0].Join();
		T[1].Join();
		time.Stop();
		Console.WriteLine($"Time with class B: {time.ElapsedMilliseconds} msec.");
	}
}

class A
{
	public int x;
	public int y;
	public A(int x, int y)
	{
		this.x = x;
		this.y = y;
	}
}

[StructLayout(LayoutKind.Explicit)]
class B
{
	[FieldOffset(0)] public int x;
	[FieldOffset(64)] public int y;
	public B(int x, int y)
	{
		this.x = x;
		this.y = y;
	}
}
// Define other methods and classes here
