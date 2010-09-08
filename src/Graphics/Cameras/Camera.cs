using Math;

namespace Graphics.Cameras
{
    /// <summary>
    /// Default camera implementation.
    /// </summary>
    public class Camera : ICamera
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class.
        /// </summary>
        /// <param name="stand">The stand which contains the view matrix.</param>
        /// <param name="lens">The lens which contains the projection matrix.</param>
        public Camera(IStand stand, ILens lens)
        {
            Stand = stand;
            Lens = lens;
        }

        /// <summary>
        /// Gets or sets the stand.
        /// </summary>
        /// <value>The stand.</value>
        public IStand Stand { get; set; }

        /// <summary>
        /// Gets or sets the lens.
        /// </summary>
        /// <value>The lens.</value>
        public ILens Lens { get; set; }

        /// <summary>
        /// Gets the view projection matrix.
        /// </summary>
        /// <value>The view projection matrix.</value>
        public Matrix ViewProjectionMatrix
        {
            get
            {
                return Lens.ProjectionMatrix * Stand.ViewMatrix;
            }
        }
    }
}