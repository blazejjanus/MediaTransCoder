namespace MediaTransCoder.Backend {
    public enum ImageEffects {
        [ImageEffects("BlackWhite", "colorchannelmixer = .3:.3:.3:0:.3:.3:.3:0:.3:.3:.3:0")]
        BlackWhite,
        [ImageEffects("Sepia", "colorchannelmixer=.393:.769:.189:0:.349:.686:.168:0:.272:.534:.131:0")]
        Sepia,
        [ImageEffects("Negative", "colorchannelmixer=1:0:0:0:1:0:0:0:1:0:0:0")]
        Negative
    }
}
