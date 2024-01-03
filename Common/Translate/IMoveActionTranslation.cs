﻿using TMPMS.Common.Move;

namespace TMPMS.Common.Translate;

public interface IMoveActionTranslation<Target>
{
    public Target Translate(MoveAction moveAction);

    public Task<Target> TranslateAsync(MoveAction moveAction);
}