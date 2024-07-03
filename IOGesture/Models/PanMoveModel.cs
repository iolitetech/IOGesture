using System.Text.Json.Serialization;
using IOGesture.Components;

namespace IOGesture.Models;

public class PanMoveModel
{
    public double? TouchStartX { get; set; }
    public double? TouchStartY { get; set; }
    public double? TouchMoveX { get; set; }
    public double? TouchMoveY { get; set; }
    public double? VelocityX { get; set; }
    public double? VelocityY { get; set; }
    public bool? IsSwipingHorizontal { get; set; }
    public bool? IsSwipingVertical { get; set; }
    public string? SwipingDirection { get; set; }
    
}