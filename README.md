# 3D Shapes Dataset Generator

<div align = center>
<a href = "https://github.com/aniketrajnish/3D-Shapes-Dataset-Generator/releases/tag/1.0"><img width="300px" height="300px" src= "https://github.com/aniketrajnish/3D-Shapes-Dataset-Generator/assets/58925008/c000e388-c0fa-4ebf-801d-443312fed756"></a>
</div>

This tool is designed to help users create highly-customized procedurally generated 3D shape datasets. It's build on top of my open source [Raymarching Engine](https://github.com/aniketrajnish/CS499-SDFNet/tree/main/Renderer) and runs over GPU.

## Getting Started

* Download the build file/windows installer from the [Releases](https://github.com/aniketrajnish/3D-Shapes-Dataset-Generator/releases/tag/1.0).
* If you need over the top features -
    *  Clone the repository
       ```
       git clone https://github.com/aniketrajnish/3D-Shapes-Dataset-Generator.git
       ```
    *  Edit the source file in `Unity 2020.3.30f1` or later.

https://github.com/aniketrajnish/3D-Shapes-Dataset-Generator/assets/58925008/6720667f-323b-406e-af8d-4fbeced10188

## Usage

* `Varying Camera Angle` (under development): Set to `True` if you want to assign a different camera angle to each image in the dataset. Otherwise, the camera will look at the object keeping it in the center using `transform.LookAt()`.
* `Varying Orientation`: Set to `True` if you want to assign a unique random orientation (angle) to the individual shapes in the dataset. Otherwise, the shapes will be aligned with the axis using `Quaternion.identity`.
* `Varying Position`: Set to `True` if you want to assign a unique random position to the individual shapes in a cube of dimension 2 units centered at the origin. Otherwise, the shapes will be centered at the origin.
* `Randomize Shape Count`: Set to `True` if you want to randomize the number of shapes in each image between 0 and the `Max Shape Count`. Otherwise, every image will be generated with `Max Shape Count` number of shapes.
* `Max Shape Count`: Set the maximum number of shapes that each image in the dataset should have.
* `Dataset Size`: Set the number of images to be generated in the dataset.
* `Dataset Path`: Set the path where the dataset folder is to be created.
* `Resolution`: Set the width and height of the images (in pixels) to generate them accordingly.
* `Shapes and Operations`: These are enums that determine which shape index and operation index are to be taken into * consideration while generating each shape.
* `Seed`: Input a seed value to generate a dataset that has already been created before by assigning the seed value to the Random State.

Note that the `Varying Camera Angle` parameter is still under development and may not be fully functional.

## Contributing

If you find a bug or have a feature request, please open an issue or submit a pull request.

## License

This project is licensed under the MIT License.
