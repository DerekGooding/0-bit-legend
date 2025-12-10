# Issue 01: Entity Editing Functionality Review

## Problem Description
The entity editing functionality in the `BitLegend.MapEditor` project currently suffers from two main deficiencies, significantly impacting usability:
1.  **Lack of Interactive Drag for Entities:** After an `EntityData` object is placed on the map, there is no direct means for users to drag and reposition it interactively using the mouse. The existing drag-and-resize mechanisms are exclusively implemented for `TransitionData` objects.
2.  **Incorrect or Absent Sizing of Selection/Bounding Box for Entities:** When an entity is selected, there is no visual feedback, such as a correctly sized bounding box, that reflects its dimensions within the game world. Furthermore, the `EntityData` model itself does not contain explicit width or height properties, which hinders the ability to define and manipulate their visual size. The `SelectionAdorner` present in the project is utilized for general map area selection, not for individual entity highlighting or manipulation.

## Root Causes

1.  **Model Limitation (`EntityData`):** The `BitLegend.MapEditor\Model\EntityData.cs` model is limited to `EntityType`, `X`, `Y`, and `Condition` properties. It conspicuously lacks `Width` and `Height` (or equivalent size) properties. These are fundamental for visually representing an entity's physical dimensions and enabling interactive resizing operations.
2.  **Adorner Misapplication and Absence:**
    *   The `ResizeAdorner` (`BitLegend.MapEditor\Adorners\ResizeAdorner.cs`), which provides both resizing and moving capabilities, is strictly typed to `TransitionData` contexts. Its logic relies on `SizeX` and `SizeY` properties specific to `TransitionData`, making it unusable for `EntityData` in its current form.
    *   There is no dedicated adorner attached to visual representations of `EntityData` objects. The `SelectionAdorner` (`BitLegend.MapEditor\Adorners\SelectionAdorner.cs`) is used for a distinct purpose—drawing a temporary selection rectangle for map cells, not for persistent entity selection or manipulation.
3.  **Generic UI Element for Entities:** The current implementation likely renders entities using generic UI elements (e.g., `TextBox` as suggested by `MapCharacterViewModel` usage) that lack inherent interactive drag/resize capabilities. Without a custom control tailored to `EntityData`, attaching and managing interactive adorners becomes complex and non-idiomatic.

## Recommendations

To address these issues and enhance the entity editing experience, the following changes are recommended:

1.  **Enhance `EntityData` Model:**
    *   **Action:** Modify `BitLegend.MapEditor\Model\EntityData.cs` to include `Width` and `Height` (or `SizeX`, `SizeY` for consistency with `TransitionData` if entities are cell-based) properties. These integer properties should represent the entity's dimensions in map cells.
    *   **Impact:** This will provide the necessary data foundation for defining an entity's visual size and enabling resizing operations.
    *   **Follow-up:** Update `EntityEditorViewModel.cs` and `MainWindowViewModel.cs` to manage these new properties in the UI.

2.  **Create a Dedicated `EntityControl` Visual Element:**
    *   **Action:** Develop a new `UserControl` or `ContentControl`, named `EntityControl.xaml`, specifically designed to display and interact with `EntityData` objects.
    *   **Details:** This control should:
        *   Bind its `Width` and `Height` properties to the new `Width` and `Height` properties of the `EntityData` (via `DataContext`).
        *   Position itself on the map (likely a `Canvas` in `MainWindow.xaml`) based on the `X` and `Y` properties of its `EntityData` `DataContext`.
        *   Contain an appropriate visual representation (e.g., an `Image` for the entity's sprite or a `TextBlock` for its character representation).
    *   **Impact:** Provides a proper target for adorners and better visual representation of entities.

3.  **Implement a Generic `IResizableAndMovable` Interface and Adapt `ResizeAdorner`:**
    *   **Action:** Create a new interface, e.g., `IResizableAndMovable`, with properties like `PositionX`, `PositionY`, `SizeX`, `SizeY`. Both `TransitionData` and the enhanced `EntityData` should implement this interface.
    *   **Action:** Modify the `ResizeAdorner` (`BitLegend.MapEditor\Adorners\ResizeAdorner.cs`) constructor and logic to accept `IResizableAndMovable` as its data context. This will make the existing resize/move adorner generic and reusable.
    *   **Impact:** Centralizes the adorner logic, reducing code duplication and ensuring consistent behavior for resizable/movable elements. This will also provide the drag handler for entities.

4.  **Integrate Adorner into `MainWindow.xaml.cs` for Entity Selection:**
    *   **Action:** Update the selection logic within `MainWindow.xaml.cs` to identify when an `EntityControl` is clicked or selected.
    *   **Details:**
        *   When an `EntityControl` is selected, retrieve the `AdornerLayer` for that control.
        *   Instantiate the now-generic `ResizeAdorner` (or a new `EntityAdorner` if a separate one is preferred) and attach it to the selected `EntityControl`.
        *   Implement logic to ensure that only one `IResizableAndMovable` element (either a `TransitionData` or `EntityData` visual) has an active adorner at any given time.
    *   **Impact:** Enables interactive dragging and resizing for entities, providing crucial visual feedback and manipulation capabilities.

5.  **Refine Selection and Interaction Logic:**
    *   **Action:** Review and refine the `MainWindow.xaml.cs` logic to clearly distinguish between map cell selection (for brush painting) and individual interactive entity selection/manipulation.
    *   **Impact:** Ensures a clear and intuitive user experience where different modes of interaction (painting vs. entity editing) are well-defined.
    
This set of recommendations provides a structured approach to enhance the entity editing capabilities, making the map editor more robust and user-friendly.