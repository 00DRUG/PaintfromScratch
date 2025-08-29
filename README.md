# Paint from Scratch

Paint from Scratch is a Windows Forms application built with C# (.NET 8) that provides a multi-tabbed, layer-based drawing experience. It supports freehand drawing, shape creation, erasing, color selection, history management.

## Features

- **Multi-Tab Canvas:** Create and manage multiple canvases in separate tabs. Each tab can be renamed, saved, or closed individually.
- **Custom Brush Tool:** Draw with configurable brush shapes (Circle, Square, Triangle, Star), color, thickness, and spacing.
- **Eraser Tool:** Erase parts of your drawing with adjustable size.
- **Shape Drawing:** Add rectangles and ellipses with customizable color and thickness. Preview shapes before applying.
- **Shape Manipulation:** Move and resize shapes after creation using manipulation handles.
- **Background Fill:** Fill areas with a selected color using a flood fill algorithm.
- **Color Picker:** Select colors using an interactive color wheel or RGB sliders. Preview the selected color.
- **History Panel:** Undo actions and restore previous states of your canvas. 
- **File Operations:** Open, save, and export canvases in PNG, JPEG, or BMP formats.
- **Tab Management:** Rename, close, and save tabs individually, with prompts for unsaved changes.
- **Checkerboard Background:** Visualize transparency with a checkerboard pattern.
- **Zoom & Pan:** Zoom in/out with the mouse wheel and pan the canvas by holding Space and dragging.


## Usage

### Main Tools

- **Brush Tool:** Click the brush icon to draw freehand. Adjust color, thickness, and shape from the controls.
- **Eraser Tool:** Click the eraser icon to erase. Adjust eraser size as needed.
- **Shape Tool:** Select rectangle or ellipse from the shape menu, then click and drag on the canvas.
- **Manipulate Tool:** Click the manipulate icon, then select a shape to move or resize. Use the handles for resizing.
- **Background Fill:** Click the fill icon, then click on the canvas to fill an area with the selected color.
- **History Panel:** Toggle the history panel to view and restore previous states.
- **Apply Button:** Apply figures to the main canvas to be able to fill them or not to mess them with further manipulation.
- **Undo Tool:** Get one action back in your timeline to correct mistakes.
- **Redo Tool:** Can restore history to some further states to correct the mistakes and be back on track.

### File Operations

- **New Canvas:** Click __New__ and enter desired width and height.
- **Open Image:** Click __Open__ to import an image file.
- **Save Canvas:** Click __Save__ to export the current tab as PNG, JPEG, or BMP.
- **Close Tab:** Click the close button on a tab; you’ll be prompted to save if there are unsaved changes.

## Features

### Multi-Tab Canvas
- Create and manage multiple canvases in separate tabs.
- Each tab is independent and can be renamed, saved, or closed individually.

### Drawing Tools

#### Brush Tool
- Draw freehand lines and shapes on the canvas.
- Customize brush color using the color wheel or RGB sliders.
- Adjust brush thickness and select brush shape: Circle, Square, Triangle, or Star /other shapes can be added easily.
- Control brush spacing for dotted or continuous strokes.

#### Eraser Tool
- Erase parts of your drawing with adjustable eraser size.
- Works like the brush tool but removes content from the canvas.

#### Shape Tool
- Draw rectangles and ellipses by selecting the shape and dragging on the canvas /other figures can be added easily.
- Set color and thickness for each shape.
- Preview shapes before applying them to the canvas.

#### Manipulate Tool
- Select, move, and resize existing shapes.
- Use resize handles to adjust shape dimensions.
- Apply changes to commit the manipulation.

#### Background Fill Tool
- Fill an area with the selected color using a flood fill algorithm.
- Click on the canvas to fill a region with the current brush color.

### Color Selection
- Use the color wheel to pick any color visually.
- Fine-tune colors with RGB sliders.
- Preview the selected color before applying.

### History Panel
- Automatically records every action (drawing, erasing, shape creation, manipulation, fill).
- Restore previous states by clicking on history entries.
- Undo functionality for safe experimentation.

### File Operations
- **New Canvas:** Create a new canvas with custom dimensions.
- **Open Image:** Import PNG, JPEG, or BMP files into a new tab.
- **Save Canvas:** Export your artwork in PNG, JPEG, or BMP format.
- **Close Tab:** Close tabs with optional save prompts.

### Canvas Display
- Transparent areas are shown with a checkerboard pattern for clarity.
- Canvases are displayed in a scalable PictureBox for accurate editing.

## Project Structure

- `MainClasses/MainWindow.cs` – Main application logic and UI event handling.
- `DrawingClasses/Shape.cs` – Shape definitions and drawing logic.
- `ToolClasses/CustomBrush.cs` – Custom brush implementation.
- `ToolClasses/EraserTool.cs` – Eraser tool logic.
- `Extensions/CustomHistory.cs` – History management.
- `MainClasses/MainWindow.Designer.cs` – UI layout and control initialization.
- `MainClasses/MainWindow.resx` – Resource file for UI elements.

## Customization

- **Brush Shapes:** Add new shapes by extending `CustomBrush`.
- **Shape Types:** Add new shapes by extending `ShapeType` and `ShapeFactory`.
- **History:** Adjust history depth or UI in `CustomHistory`.

### Prerequisites

- Visual Studio 2022 or later
- .NET 8 SDK

### Installation

1. **Clone the repository:**
2. **Open the solution:**
- Launch Visual Studio and open `PaintfromScratch.sln`.

3. **Restore NuGet packages:**
- Visual Studio will automatically restore required packages on build.

4. **Build and run:**
- Press `F5` or click __Start Debugging__ to launch the application.

## Contributing

Pull requests are welcome! For major changes, please open an issue first to discuss what you would like to change.
