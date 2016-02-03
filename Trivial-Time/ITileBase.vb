Public Interface ITileBase
    Inherits IFrameworkInputElement

    Property Category As category
    Property Neighbors As List(Of ITileBase)
    Shadows Property IsEnabled As Boolean
    Sub FlashUp(Optional duration As Integer = 0)
    Sub FlashDown()

End Interface