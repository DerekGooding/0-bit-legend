# Issue 02: Jittery Dragging and Resizing of AreaTransitions

## Problem Description
When users attempt to drag or resize an `AreaTransition` (represented by a blue `Rectangle` on the map) using the `ResizeAdorner`, the visual feedback is "jittery." The `Rectangle` does not move or resize smoothly with the mouse cursor; instead, it appears to jump or snap in discrete steps. This significantly degrades the user experience for manipulating map transitions.

## Root Cause
The core reason for the jittery behavior is a fundamental mismatch in precision and rounding between how the `AreaTransition`'s visual representation is rendered and how its underlying model properties are updated during a drag or resize operation.

1.  **Pixel-Perfect UI Rendering (Fractional Precision):**
    *   The `Rectangle` elements representing `AreaTransition` objects are rendered in `MainWindow.xaml` using `MultiBinding` with the `PositionConverter`.
    *   The `PositionConverter`'s logic `(pos * totalSize) / count` uses `double`-precision arithmetic throughout. This means it computes `Canvas.Left`, `Canvas.Top`, `Width`, and `Height` with high precision, allowing the `Rectangle` to be positioned and sized at fractional pixel values, leading to potentially smooth visual movement if updated frequently.

2.  **Aggressive Cell-Based Rounding in `ResizeAdorner` (Integer Precision):**
    *   The `ResizeAdorner.HandleDrag` method, which processes the mouse's drag delta, receives pixel-based changes (`e.HorizontalChange`, `e.VerticalChange`).
    *   It then converts these pixel changes into cell-based changes (`dx`, `dy`) using the `_cellSize` (derived from the `ActualWidth`/`ActualHeight` of a map `TextBox`).
    *   Crucially, the result of this conversion is immediately cast to an `int` using `(int)Math.Round()`. This forces all updates to the `TransitionData` model's `PositionX`, `PositionY`, `SizeX`, and `SizeY` properties to be whole integer cell values.

3.  **The Disparity and Jitter:**
    *   The user's mouse provides continuous, sub-pixel movement.
    *   However, due to the `(int)Math.Round()` in `ResizeAdorner`, the underlying `TransitionData` model only updates its cell-based properties when the accumulated pixel drag is large enough to round to a new integer cell value.
    *   This causes the smoothly moving mouse cursor to correspond to `Rectangle` updates that "snap" to the nearest whole cell boundaries, producing the observed "jittery" visual effect.

## Recommendations

To eliminate the jitter and provide a smooth dragging and resizing experience for `AreaTransition` objects, the following approaches are recommended:

1.  **Modify `ResizeAdorner` to Allow Fractional Cell Updates (Preferred Approach if Model Allows):**
    *   **Action:** Change the data types of `PositionX`, `PositionY`, `SizeX`, and `SizeY` properties within the `TransitionData` model (`BitLegend.MapEditor\Model\TransitionData.cs`) from `int` to `double`. This allows the model to store and represent fractional cell positions and sizes.
    *   **Action:** In `ResizeAdorner.cs`, remove the `(int)Math.Round()` cast from the `HandleDrag` method. Allow `dx` and `dy` to remain `double` values and apply these directly to the `_transition`'s `double`-type position and size properties.
    *   **Impact:** This ensures that the model updates reflect the smooth pixel-based mouse movement more accurately, which, when fed back through the `PositionConverter`, will result in smooth, pixel-perfect rendering of the `Rectangle`.
    *   **Consideration:** If the game's core logic absolutely requires integer cell coordinates, a snapping mechanism could be implemented when the map is saved or when `TransitionData` is consumed by the game, rather than during real-time UI manipulation.

2.  **Ensure Accurate `_cellSize` Calculation:**
    *   **Action:** Thoroughly verify that the `_cellSize.Width` and `_cellSize.Height` values passed to the `ResizeAdorner` (which are derived from `TextBox.ActualWidth`/`ActualHeight` in `MainWindow.xaml.cs`) precisely match the effective pixel size of a single map cell as understood and used by the `PositionConverter`.
    *   **Verification:** Compare `firstCell.ActualWidth` with the result of `MapItemsControl.ActualWidth / viewModel.SelectedMap.Raw[0].Length`. These values should be identical. If any discrepancies exist due to WPF layout rounding, DPI scaling, or implicit `TextBox` paddings/margins that were not set to zero, these should be accounted for in the `_cellSize` calculation or in the `PositionConverter` logic to maintain a consistent understanding of cell dimensions across the application.

By addressing the rounding and precision mismatch in the `ResizeAdorner` and ensuring a consistent cell size definition, the jittery behavior will be resolved, leading to a much smoother and more intuitive user experience.