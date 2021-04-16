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

## video

[![Everything Is AWESOME](https://img.youtube.com/vi/StTqXEQ2l-Y/0.jpg)](https://www.youtube.com/watch?v=StTqXEQ2l-Y "Everything Is AWESOME")

## Images

### Full Screen
![alt text](https://github.com/AugustinSorel/DoublePendulum/blob/master/TestingImages/Capture%20d%E2%80%99%C3%A9cran%202021-04-01%20213444.png?raw=true)

### Small Screen
![alt text](https://github.com/AugustinSorel/DoublePendulum/blob/master/TestingImages/Capture%20d%E2%80%99%C3%A9cran%202021-04-01%20213512.png?raw=true)

### Only the Double Pendulum
![alt text](https://github.com/AugustinSorel/DoublePendulum/blob/master/TestingImages/Capture%20d%E2%80%99%C3%A9cran%202021-04-01%20213240.png?raw=true)
