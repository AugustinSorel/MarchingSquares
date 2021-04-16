# MarchingSquares

## Description

Marching squares is a computer graphics algorithm that generates contours for a two-dimensional scalar field
Typical applications include the contour lines on topographic maps or the generation of isobars for weather maps.

## Pseudo Code

* Process each cell in the grid independently.
* Calculate a cell index using comparisons of the contour level(s) with the data values at the cell corners.
* Use a pre-built lookup table, keyed on the cell index, to describe the output geometry for the cell.
* Apply linear interpolation along the boundaries of the cell to calculate the exact contour position.

## Default Value

* resolution = 20
* Speed 0.03f
* increment = 0.1f

## Links
* [Wikipedia](https://en.wikipedia.org/wiki/Marching_squares)
* [Youtube](https://youtu.be/K_Rlfm4sDlg) - Testing video.

## Youtube Video

[![Everything Is AWESOME](https://github.com/AugustinSorel/MarchingSquares/blob/master/Images/Capture.PNG)](https://www.youtube.com/watch?v=ONhnbyC9sJg)

## Images

### Default Value
![alt text](https://github.com/AugustinSorel/MarchingSquares/blob/master/Images/Capture.PNG)

### Default Value with circle
![alt text](https://github.com/AugustinSorel/MarchingSquares/blob/master/Images/Capture2.PNG)

### Resolution 11
![alt text](https://github.com/AugustinSorel/MarchingSquares/blob/master/Images/Capture3.PNG)

### Increment 0.6
![alt text](https://github.com/AugustinSorel/MarchingSquares/blob/master/Images/Capture4.PNG)

### Cutting of the squares
![alt text](https://github.com/AugustinSorel/MarchingSquares/blob/master/Images/Capture6.png)
