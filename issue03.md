# Issue 03: Insufficient Visual Feedback for Button Clickability

## Problem Description
The user interface of the Map Editor fails to adequately convey the interactive state of its menu buttons. Some buttons appear to be unclickable under specific conditions, yet there is no clear visual distinction between an active, clickable button and a disabled, inactive one. This leads to user frustration and a poor understanding of available actions.

## Root Causes

1.  **Command `CanExecute` Logic:**
    *   The `MainWindowViewModel.cs` correctly implements `ICommand` patterns, utilizing `CanExecute` methods (such as `IsSelectedMap`, `IsSelectedEntity`, `IsSelectedMapAndEntity`, `IsSelectedTransition`, `IsSelectedMapAndTransition`) to control the enabled state of various buttons.
    *   For example:
        *   `DeleteMap`, `SaveMap`, `AddEntity`, `AddTransition` are only enabled when a `SelectedMap` is present.
        *   `EditEntity` is only enabled when an `SelectedEntity` is chosen.
        *   `DeleteEntity` requires both `SelectedMap` and `SelectedEntity` to be active.
        *   Similar dependencies exist for transition-related buttons.
    *   While functionally correct, this design can lead to buttons remaining perpetually disabled if their preconditions are rarely or never met during a typical user workflow (e.g., no map loaded initially, no entity selected).

2.  **Lack of Visual Feedback for Disabled State in Custom Button Template:**
    *   The `Button` elements in `MainWindow.xaml` apply a custom `Style` that includes a `ControlTemplate`. This template is defined in `DarkTheme.xaml` and `LightTheme.xaml`.
    *   Crucially, this custom `ControlTemplate` for `Button` elements **lacks a `ControlTemplate.Trigger` for the `IsEnabled="False"` state**.
    *   WPF's default button behavior typically grays out or reduces the opacity of disabled buttons. By providing a custom `ControlTemplate` without explicitly defining the visual appearance for `IsEnabled="False"`, the application loses this essential feedback.
    *   As a result, a disabled button retains the exact same visual appearance as an enabled one, making it impossible for users to know whether they can interact with it without attempting to click it.

## Recommendations

To improve the usability and clarity of the Map Editor's UI, the following actions are recommended:

1.  **Enhance Button `ControlTemplate` to Visually Indicate Disabled State:**
    *   **Action:** Modify the `ControlTemplate` for `Button` in both `BitLegend.MapEditor\Themes\DarkTheme.xaml` and `BitLegend.MapEditor\Themes\LightTheme.xaml`. Add a `ControlTemplate.Trigger` that specifically targets the `IsEnabled="False"` state.
    *   **Details:** Within this trigger, apply visual setters to the button's `Background`, `Foreground`, and potentially `BorderBrush` to make its disabled status immediately apparent. Effective visual cues include:
        *   Reducing the button's `Opacity` (e.g., to `0.4` or `0.5`).
        *   Changing the `Background` to a desaturated, muted, or grayed-out color.
        *   Changing the `Foreground` (text) color to a darker or lighter shade of gray.
    *   **Impact:** Users will instantly recognize which buttons are currently interactive, significantly improving navigation and reducing confusion.

2.  **Review and Optimize Command Preconditions and User Flow:**
    *   **Action:** Evaluate the conditions under which buttons become enabled/disabled and ensure that the application's design facilitates the user in meeting these preconditions.
    *   **Details:**
        *   Consider ensuring that a default map is loaded upon application startup, or providing a clear prompt to create a new map, so that map-related management buttons (`Save Map`, `Delete Map`, `Add Entity`, `Add Transition`) are active from the outset.
        *   As discussed in `issue01.md`, implement a clear and intuitive mechanism for selecting entities and transitions on the map. This will ensure that `SelectedEntity` and `SelectedTransition` properties are reliably set, thereby enabling their respective edit/delete buttons when an item is selected.
    *   **Impact:** Prevents situations where buttons are perceived as "never clickable" by guiding the user towards fulfilling the necessary criteria for interaction, enhancing discoverability and overall user experience.