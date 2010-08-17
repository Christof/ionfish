using Math;

namespace Graphics.Cameras
{
    /// <summary>
    /// Camera, which is the 'window' to the 3d-world.
    /// </summary>
    public interface ICamera
    {
        /// <summary>
        /// Gets or sets the stand.
        /// </summary>
        /// <value>The stand.</value>
        IStand Stand { get; set; }

        /// <summary>
        /// Gets or sets the lens.
        /// </summary>
        /// <value>The lens.</value>
        ILens Lens { get; set; }

        /// <summary>
        /// Gets the view projection matrix.
        /// </summary>
        /// <value>The view projection matrix.</value>
        Matrix ViewProjectionMatrix { get; }
    }
}