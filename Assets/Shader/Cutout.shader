Shader "Custom/Cutout" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
    }

    SubShader {
        Tags {"Queue" = "Transparent"}

        Pass{
            Zwrite On
            ColorMask 0
        }
    }
}