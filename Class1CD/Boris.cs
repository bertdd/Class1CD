using System.Drawing;

namespace MauiBreakout2;

internal class Boris : GameObject
{
  public Boris()
  {
    Source = "boris.png";
    HorizontalOptions = LayoutOptions.Center;
  }

  internal void Move(AbsoluteLayout gameArea, double? deltaX)
  {
    if (deltaX != null)
    {
      var rect = gameArea.GetLayoutBounds(this);
      var x = (double)((gameArea.Width - rect.Width) / 2 + deltaX * gameArea.Width / 2);
      x = Math.Min(Math.Max(x, 0), gameArea.Width - rect.Width);
      gameArea.SetLayoutBounds(this, new Rect(x, rect.Y, rect.Width, rect.Height));
    }
  }

  int jumpHeight = 0;
  bool Jumping = false;
  bool Up = false;
  double startY = -1;

  internal void Jump(AbsoluteLayout gameArea, int jump = 100, int step = 4)
  {
    if (!Jumping)
    {
      Jumping = true;
      Up = true;
      var r = gameArea.GetLayoutBounds(this);
      if (startY < 0)
      {
        startY = r.Y;
      }
    }
    if (Up)
    {
      jumpHeight += step;
      if (jumpHeight > jump)
      {
        Up = false;
      }
    }
    else
    {
      jumpHeight -= step;
      if (jumpHeight <= 0)
      {
        Jumping = false;
        Up = true;
      }
    }
    var rectangle = gameArea.GetLayoutBounds(this);

    var jumpTo = new Rect(rectangle.X, startY - jumpHeight,
      rectangle.Width, rectangle.Height);

    gameArea.SetLayoutBounds(this, jumpTo);

  }
}
