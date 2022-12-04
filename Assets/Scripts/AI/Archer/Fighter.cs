namespace AI.Archer
{
    public class Fighter
    {
        public Fighter(Arch arch)
        {
            _arch = arch;
        }

        public void TryShoot()
        {
            _arch.TryShoot();
        }

        private Arch _arch;
    }
}
