/*
 *	作成者     :
 *	機能説明   :
 * 	初回作成日 :
 *	最終更新日 :
 */

namespace Village {

    [System.Serializable]
    public class MoveLimit{
        public float min = 0;
        public float max = 0;

        public MoveLimit() {

        }

        public MoveLimit(float _min,float _max) {
            min = _min;
            max = _max;
        }
    }

}