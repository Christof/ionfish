using Math;

namespace Graphics.Cameras
{
    /// <summary>
    /// The stand defines the position and orientation of the camera.
    /// </summary>
    public interface IStand
    {
        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <value>The position.</value>
        Vector3 Position { get; }

        /// <summary>
        /// Gets the direction.
        /// </summary>
        /// <value>The direction.</value>
        Vector3 Direction { get; }

        /// <summary>
        /// Gets a vector point upwards.
        /// </summary>
        /// <value>The up vector.</value>
        Vector3 Up { get; }

        /// <summary>
        /// Gets the view matrix.
        /// </summary>
        /// <value>The view matrix.</value>
        Matrix ViewMatrix { get; }
    }
}