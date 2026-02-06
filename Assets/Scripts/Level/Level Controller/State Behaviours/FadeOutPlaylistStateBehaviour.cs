public class FadeOutPlaylistStateBehaviour : StateBehaviour
{
    private PlaylistController _playlistController;

    public FadeOutPlaylistStateBehaviour(PlaylistController playlistController)
    {
        _playlistController = playlistController;
    }

    public override void Enter(float transitionDuration)
    {
        _playlistController.FadeVolumeOut(transitionDuration);
    }
}