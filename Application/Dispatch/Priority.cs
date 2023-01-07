namespace UIGL.Application.Dispatch {
    public enum Priority {
        AppPre,
        InputPre,
        InputPost,
        RenderPre,
        RenderPost,
        AppIdle,
        ContextIdle,
        AppPost
    }
}