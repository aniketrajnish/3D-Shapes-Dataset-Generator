# 3D Shapes Dataset Generator

<div align = center>
<a href = "https://github.com/aniketrajnish/3D-Shapes-Dataset-Generator/releases/tag/1.0"><img width="300px" height="300px" src= "https://github.com/aniketrajnish/3D-Shapes-Dataset-Generator/assets/58925008/749e9463-8cd8-4f10-bcb0-ee8502ce946b"></a>
</div>

This tool is designed to help users create procedurally generated 3D shape datasets customized to their needs. It's built on top of my open source [Raymarching Engine](https://github.com/aniketrajnish/CS499-SDFNet/tree/main/Renderer) and runs over GPU. The engine supports over thirty primitives, three operations (Union, Intersection, and Subtraction), and varying color values (along with shadows).

<p align="center">  
  <img src="https://github.com/aniketrajnish/3D-Shapes-Dataset-Generator/assets/58925008/4f245e7e-36d9-48ca-a34f-abc9d5d1d6be" width="50%" alt="3" style="border: 10px solid black; border-radius: 100px;"><br>
  A glimpse of the Final Dataset
</p>
<p align="center">
  <img src="https://github.com/aniketrajnish/3D-Shapes-Dataset-Generator/assets/58925008/3817574b-e8a2-43fa-a694-9f42faa3d324" width="50%" alt="op" style="border: 10px solid black; border-radius: 100px;"><br>
  Sample image with operations: Cone (Union) + Sphere (Union) + Box Frame (Subtraction)
</p>

## Getting Started

* Download the build file/windows installer from the [Releases](https://github.com/aniketrajnish/3D-Shapes-Dataset-Generator/releases/tag/1.0).
* If you need over the top features -
    *  Clone the repository
       ```
       git clone https://github.com/aniketrajnish/3D-Shapes-Dataset-Generator.git
       ```
    *  Open and edit the source code (in the `/src` folder) in `Unity 2020.3.30f1` or later. 

https://github.com/aniketrajnish/3D-Shapes-Dataset-Generator/assets/58925008/b6953128-a2d6-4023-b9ff-02ea1e3d8935

## Usage

* `Varying Camera Angle` (currently not functional): Set to `True` if you want to assign a different camera angle to each image in the dataset. Otherwise, the camera will look at the object keeping it in the center using `transform.LookAt()`.
* `Varying Orientation`: Set to `True` if you want to assign a unique random orientation (angle) to the individual shapes in the dataset. Otherwise, the shapes will be aligned with the axis using `Quaternion.identity`.
* `Varying Position`: Set to `True` if you want to assign a unique random position to the individual shapes in a cube of dimension 2 units centered at the origin. Otherwise, the shapes will be centered at the origin.
* `Randomize Shape Count`: Set to `True` if you want to randomize the number of shapes in each image between 0 and the `Max Shape Count`. Otherwise, every image will be generated with `Max Shape Count` number of shapes.
* `Max Shape Count`: Set the maximum number of shapes that each image in the dataset should have.
* `Dataset Size`: Set the number of images to be generated in the dataset.
* `Dataset Path`: Set the path where the dataset folder is to be created.
* `Resolution`: Set the width and height of the images (in pixels) to generate them accordingly.
* `Shapes and Operations`: These are enums that determine which shape index and operation index are to be taken into * consideration while generating each shape.
* `Seed`: Input a seed value to generate a dataset that has already been created before by assigning the seed value to the Random State.

Note that the `Varying Camera Angle` parameter is still under development and is not functional.

## Datasets
* The images are saved in the `../images` folder.
* The seed value of each random state is also exported in a txt file and can be used to
re-generate a dataset.
* These parameters are exported in the CSV sheet with all the image information as shown in table below.

| Column Name  | Info |
| ------------- | ------------- |
| filename  | Name of the image file  |
| shape  | Shape Index  |
| operation  | Operation Index |
| a,b,c,d,e,f,g,h,i,j,k,l  | Dimensional parameters |
| hue, sat, val  | HSV Values of the color |
| rot_x, rot_y, rot_z  | Euler Angles |
| pos_x, pos_y, pos_z | Position Vector |

* Each row depicts information about a shape in the image of a dataset.

## Contributing

If you find a bug or have a feature request, please open an issue or submit a pull request.

## License

This project is licensed under the MIT License.
